using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using System.Security.Claims;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public TeamController(IUnitOfWork unitOfWork,
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
        //[HttpPost]
        //public IActionResult Index(Team vm)
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult GetAll()
        {
            var datalist = (from z in _unitOfWork.Team.GetAll().ToList()
                            select new
                            {
                                id = z.Id,
                                name = z.Name,
                                jobs = z.Jobs,
                                position = z.Position,
                            }).ToList().OrderByDescending(o => o.id);

            return Json(new { data = datalist });
        }
        public IActionResult Upsert(int? id)
        {
            var GetFlag = _unitOfWork.Team.GetAll().Where(z => z.Id != id).ToList();
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

            ViewBag.ListPosistion = new SelectList(itemlist);
            Team vm = new Team();
            if (id == null)
            {
                return View(vm);
            }
            vm = _unitOfWork.Team.GetFirstOrDefault(b => b.Id == id);
            if (vm == null)
            {
                return NotFound();
            }


            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Team vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Position != null)
                {
                    vm.Flag = Convert.ToInt32(vm.Position);
                }
                if (vm.Id == 0)
                {
                    _unitOfWork.Team.Add(vm);
                    TempData["success"] = "Team created successfully";
                }
                else
                {

                    _unitOfWork.Team.Update(vm);
                    TempData["success"] = "Team updated successfully";
                }
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, @"images\team");
                    string fileName = "team-" + vm.Position;
                    var extenstion = ".jpg";

                    if (vm.Photo != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(uploads, vm.Photo.TrimStart('\\'));
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
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Team.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Team";
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Team.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Team successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }
        [HttpPost]
        public async Task<IActionResult> UpsertHeader(string id)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                MsUDC msUDC = _unitOfWork.MsUDC.GetAll().Where(z=>z.EntryKey == "Header" && z.Text1 == "MenuTeam").FirstOrDefault();

                if (msUDC != null)
                {
                    msUDC.Text3 = id;
                    msUDC.Creator = claimsIdentity.Name;
                    msUDC.LastModifyDate = DateTime.Now;
                    _unitOfWork.MsUDC.Update(msUDC);
                }
                else
                { 
                    MsUDC newmsUDC = new MsUDC();
                    newmsUDC.EntryKey = "Header";
                    newmsUDC.Text1 = "MenuTeam";
                    newmsUDC.Text3 = id;
                    newmsUDC.Creator = claimsIdentity.Name;
                    newmsUDC.LastModifyDate = DateTime.Now;
                    _unitOfWork.MsUDC.Add(newmsUDC);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Successfully Update Header";
                return Json(new { success = true, message = "Update Header Successful" });
            }
            catch (Exception)
            {
                TempData["Failed"] = "Error Update Header";
                return Json(new { success = false, message = "Update Header Error" });
            }
         
        }
    }
}
