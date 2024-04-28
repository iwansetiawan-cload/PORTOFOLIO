using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using PORTOFOLIO.Utility;
using System.Data;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AboutUsController : Controller
    {
      
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AboutUsController(IUnitOfWork unitOfWork,
        IWebHostEnvironment hostEnvironment
        )
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
          
            AboutUs aboutus = _unitOfWork.AboutUs.GetFirstOrDefault();

            if (aboutus == null)
            {
                return View();
            }

            return View(aboutus);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AboutUs aboutus)
        {

            var errorval = ModelState.Values.SelectMany(i => i.Errors);
            //if (ModelState.IsValid)
            //{

                if (aboutus.Id == 0)
                {
                    _unitOfWork.AboutUs.Add(aboutus);
                    TempData["success"] = "About Us created successfully";
                }
                else
                {                  

                    _unitOfWork.AboutUs.Update(aboutus);
                    TempData["success"] = "About Us updated successfully";
                }
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, @"images\aboutus");
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = ".jpg";
                    string filenames = "";
                    if (aboutus.Photo != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, aboutus.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var imagePathOld = Path.Combine(webRootPath, aboutus.Photo.TrimStart('\\').Replace("_1920x1080.jpg", ".jpg"));
                        if (System.IO.File.Exists(imagePathOld))
                        {
                            System.IO.File.Delete(imagePathOld);
                        }
                    }                  

                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    Resize(Path.Combine(uploads, fileName + extenstion), 1920, 1080);

                    filenames = @"\images\aboutus\" + fileName + extenstion;
                    aboutus.Photo = filenames.Replace(".jpg", "_1920x1080.jpg");
                }
                _unitOfWork.Save();
                //return RedirectToAction(nameof(Index));
            //}
            return View(aboutus);
        }
        public static void Resize(string srcPath, int width, int height)
        {
            Image image = Image.FromFile(srcPath);
            Bitmap resultImage = Resize(image, width, height);
            resultImage.Save(srcPath.Replace(".jpg", "_" + width + "x" + height + ".jpg"));
        }
        public static Bitmap Resize(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
