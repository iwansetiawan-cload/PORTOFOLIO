using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.DataAccess.Repository;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using System.Diagnostics;

namespace PORTOFOLIO.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork; 

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.TitleSlide1 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "1").Select(z => z.Title).FirstOrDefault();
            ViewBag.TextSlide1 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "1").Select(z => z.Content).FirstOrDefault();

            ViewBag.TitleSlide2 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "2").Select(z => z.Title).FirstOrDefault();
            ViewBag.TextSlide2 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "2").Select(z => z.Content).FirstOrDefault();

            ViewBag.TitleSlide3 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "3").Select(z => z.Title).FirstOrDefault();
            ViewBag.TextSlide3 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "3").Select(z => z.Content).FirstOrDefault();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}