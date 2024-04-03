using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                if (branda.Id == 0)
                {
                    _unitOfWork.Branda.Add(branda);
                    TempData["success"] = "Branda created successfully";
                }
                else
                {
                    _unitOfWork.Branda.Update(branda);
                    TempData["success"] = "Branda updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(branda);
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
