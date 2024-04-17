using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
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
          
            AboutUs aboutus = new AboutUs();
            //if (id == null)
            //{
            //    return View());
            //}

            aboutus = _unitOfWork.AboutUs.GetFirstOrDefault();
            if (aboutus == null)
            {
                return NotFound();
            }
            return View(aboutus);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AboutUs aboutus)
        {
            if (ModelState.IsValid)
            {

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
                    string fileName = "aboutUs";
                    var extenstion = ".jpg";

                    if (aboutus.Photo != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, aboutus.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    aboutus.Photo = fileName + extenstion;
                }
                _unitOfWork.Save();
                //return RedirectToAction(nameof(Index));
            }
            return View(aboutus);
        }
    }
}
