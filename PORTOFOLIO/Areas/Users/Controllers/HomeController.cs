using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.DataAccess.Repository;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using System.Diagnostics;
using System.Reflection.Metadata;

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
            const int pageSize = 100;
            ViewBag.TitleSlide1 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "1").Select(z => z.Title).FirstOrDefault();
            string textSlide1 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "1").Select(z => z.Content).FirstOrDefault();
            ViewBag.TextSlide1_ = textSlide1.Substring(0, pageSize) + " . . .";
            ViewBag.TextSlide1 = textSlide1;

            ViewBag.TitleSlide2 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "2").Select(z => z.Title).FirstOrDefault();
            string textSlide2 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "2").Select(z => z.Content).FirstOrDefault();
            ViewBag.TextSlide2_ = textSlide2.Substring(0, pageSize) + " . . .";
            ViewBag.TextSlide2 = textSlide2;

            ViewBag.TitleSlide3 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "3").Select(z => z.Title).FirstOrDefault();
            string textSlide3 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "3").Select(z => z.Content).FirstOrDefault();
            ViewBag.TextSlide3_ = textSlide3.Substring(0, pageSize) + " . . .";
            ViewBag.TextSlide3 = textSlide3;

            #region About Us
            string Description = _unitOfWork.AboutUs.GetFirstOrDefault().Description;
            ViewBag.Description = Description.Length > 100 ? Description.Substring(0, pageSize) + " . . ." : Description;
            string TextVision = _unitOfWork.AboutUs.GetFirstOrDefault().Vision;
            ViewBag.TextVision = TextVision.Length > 100 ? TextVision.Substring(0, pageSize) + " . . ." : TextVision;
            string TextMision = _unitOfWork.AboutUs.GetFirstOrDefault().Mission;
            ViewBag.TextMision = TextMision.Length > 100 ? TextMision.Substring(0, pageSize) + " . . ." : TextMision;
            #endregion
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Detail()
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