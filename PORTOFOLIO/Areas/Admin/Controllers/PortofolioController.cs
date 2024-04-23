using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortofolioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PortofolioController(IUnitOfWork unitOfWork,
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
            var datalist = (from z in _unitOfWork.Portofolio.GetAll().ToList()
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
            var GetFlag = _unitOfWork.Portofolio.GetAll().Where(z => z.Id != id).ToList();
            List<string> itemlist = new List<string>();
            itemlist.Add("Left");
            itemlist.Add("Center");
            itemlist.Add("Right");

            List<string> numbers = new List<string>();
            if (GetFlag.Where(z => z.Flag == 1).Count() < 3)
            {
                numbers.Add("1");               
            }
            if (GetFlag.Where(z => z.Flag == 2).Count() < 3)
            {
                numbers.Add("2");                
            }
            if (GetFlag.Where(z => z.Flag == 3).Count() < 3)
            {
                numbers.Add("3");                
            }           

            ViewBag.ListPosistion = new SelectList(itemlist);
            ViewBag.ListNumber = new SelectList(numbers);
            Portofolio vm = new Portofolio();
            if (id == null)
            {
                return View(vm);
            }
            vm = _unitOfWork.Portofolio.GetFirstOrDefault(b => b.Id == id);
            if (vm == null)
            {
                return NotFound();
            }

            
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Portofolio vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == 0)
                {
                    _unitOfWork.Portofolio.Add(vm);
                    TempData["success"] = "Portfolio created successfully";
                }
                else
                {                    

                    _unitOfWork.Portofolio.Update(vm);
                    TempData["success"] = "Portfolio updated successfully";
                }
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, @"images\portfolio");
                    //string fileName = Convert.ToString(vm.Flag) + "-" + vm.Position;
                    //var extenstion = ".jpg";
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[0].FileName);

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
                    vm.Photo = @"\images\portfolio\" +  fileName + extenstion;
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Portofolio.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Portfolio";
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Portofolio.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Portfolio successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}
