using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using PORTOFOLIO.Utility;
using System.Data;
using System.Linq;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ServicesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ServicesController(IUnitOfWork unitOfWork,
            IWebHostEnvironment hostEnvironment)
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
            var datalist = (from z in _unitOfWork.Services.GetAll().ToList()
                            select new
                            {
                                id = z.Id,
                                title = z.Title,
                                content = z.Content,
                            }).ToList().OrderByDescending(o => o.id);

            return Json(new { data = datalist });
        }
        public IActionResult Upsert(int? id)
        {
            var GetFlag = _unitOfWork.Services.GetAll().ToList();
            List<string> itemlist = new List<string>();          

            if (GetFlag.Where(z => z.Flag == 1).Count() == 0)
            {
                itemlist.Add("1");
            }
            if (GetFlag.Where(z => z.Flag == 2).Count() == 0)
            {
                itemlist.Add("2");
            }
            if (GetFlag.Where(z => z.Flag == 3).Count() == 0)
            {
                itemlist.Add("3");
            }
            if (GetFlag.Where(z => z.Flag == 4).Count() == 0)
            {
                itemlist.Add("4");
            }
            if (GetFlag.Where(z => z.Flag == 5).Count() == 0)
            {
                itemlist.Add("5");
            }
            if (GetFlag.Where(z => z.Flag == 6).Count() == 0)
            {
                itemlist.Add("6");
            }

            ViewBag.ListPosistion = new SelectList(itemlist);
            Services vm = new Services();
            if (id == null)
            {
                return View(vm);
            }
            vm = _unitOfWork.Services.GetFirstOrDefault(b => b.Id == id);
            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Services vm)
        {
            if (ModelState.IsValid)
            {

                if (vm.Id == 0)
                {
                    _unitOfWork.Services.Add(vm);
                    TempData["success"] = "Services created successfully";
                }
                else
                {
                    string webRootPath = _hostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        string fileName = vm.Title;
                        var extenstion = ".jpg";
                       

                        if (vm.Photo != null)
                        {
                            //this is an edit and we need to remove old image
                            var imagePath = Path.Combine(webRootPath, vm.Photo.TrimStart('\\'));
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                        }
                        using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                        {
                            files[0].CopyTo(filesStreams);
                        }
                        vm.Photo = fileName + extenstion;
                    }

                    _unitOfWork.Services.Update(vm);
                    TempData["success"] = "Services updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Services.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Services";
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Services.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Services successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}
