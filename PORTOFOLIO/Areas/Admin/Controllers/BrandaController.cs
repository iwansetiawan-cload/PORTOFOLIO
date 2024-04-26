using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
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
    public class BrandaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BrandaController(IUnitOfWork unitOfWork,
            IWebHostEnvironment hostEnvironment
            )
        {
			_unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {         
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var datalist = (from z in _unitOfWork.Branda.GetAll().ToList()
                            select new
                            {
                                id = z.Id,
                                title = z.Title,
                                content = z.Content,
                                position = z.Position,
                            }).ToList().OrderByDescending(o => o.id);

            return Json(new { data = datalist });
        }
        public IActionResult Upsert(int? id)
        {
            var GetFlag = _unitOfWork.Branda.GetAll().Where(z => z.Id != id).ToList();
            List<string> numbers = new List<string>();
            if (GetFlag.Where(z => z.Flag == 1).Count() == 0)
            {
                numbers.Add("1");
            }
            if (GetFlag.Where(z => z.Flag == 2).Count() == 0)
            {
                numbers.Add("2");
            }
            if (GetFlag.Where(z => z.Flag == 3).Count() == 0)
            {
                numbers.Add("3");
            }
            ViewBag.ListNumber = new SelectList(numbers);
            Branda branda = new Branda();
            if (id == null)
            {
                return View(branda);
            }
            branda = _unitOfWork.Branda.GetFirstOrDefault(b => b.Id == id);
            if (branda == null)
            {
                return NotFound();
            }
            return View(branda);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Branda branda)
        {
            if (ModelState.IsValid)
            {
                if (branda.Flag != null)
                    branda.Position = Convert.ToString(branda.Flag);
                if (branda.Id == 0)
                {
                    _unitOfWork.Branda.Add(branda);
                    TempData["success"] = "Home created successfully";
                }
                else
                {       
                    _unitOfWork.Branda.Update(branda);
                    TempData["success"] = "Home updated successfully";
                }
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, @"images\slide");
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = ".jpg";
                    string filenames = "";                             

                    if (branda.Photo != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, branda.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var imagePathOld = Path.Combine(webRootPath, branda.Photo.TrimStart('\\').Replace("_1920x1080.jpg", ".jpg"));
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

                    filenames = @"\images\slide\" + fileName + extenstion;
                    branda.Photo = filenames.Replace(".jpg", "_1920x1080.jpg");
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(branda);
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
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Branda.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Branda";
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Branda.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Branda successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}
