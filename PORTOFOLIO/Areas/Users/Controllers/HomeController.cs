using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.DataAccess.Repository;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using System.Diagnostics;
using System.Drawing.Printing;
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

            #region Service
            ViewBag.Servis_Title1 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 1).Select(z => z.Title).FirstOrDefault();
            ViewBag.Servis_Desc1 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 1).Select(z => z.Content).FirstOrDefault();

            ViewBag.Servis_Title2 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 2).Select(z => z.Title).FirstOrDefault();
            ViewBag.Servis_Desc2 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 2).Select(z => z.Content).FirstOrDefault();

            ViewBag.Servis_Title3 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 3).Select(z => z.Title).FirstOrDefault();
            ViewBag.Servis_Desc3 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 3).Select(z => z.Content).FirstOrDefault();

            ViewBag.Servis_Title4 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 4).Select(z => z.Title).FirstOrDefault();
            ViewBag.Servis_Desc4 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 4).Select(z => z.Content).FirstOrDefault();

            ViewBag.Servis_Title5 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 5).Select(z => z.Title).FirstOrDefault();
            ViewBag.Servis_Desc5 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 5).Select(z => z.Content).FirstOrDefault();

            ViewBag.Servis_Title6 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 6).Select(z => z.Title).FirstOrDefault();
            ViewBag.Servis_Desc6 = _unitOfWork.Services.GetAll().Where(z => z.Flag == 6).Select(z => z.Content).FirstOrDefault();
            #endregion

            #region Portfolio
            ViewBag.Portfolio_Title1 = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 1).Select(z => z.Title).FirstOrDefault();
            ViewBag.Portfolio_Title2 = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 2).Select(z => z.Title).FirstOrDefault();
            ViewBag.Portfolio_Title3 = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 3).Select(z => z.Title).FirstOrDefault();

            ViewBag.Portfolio_Desc1 = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 1 && z.Position == "Left").Select(z => z.Content).FirstOrDefault();
            #endregion

            #region Team
            ViewBag.Team_Name1 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 1).Select(z => z.Name).FirstOrDefault();
            ViewBag.Team_Jobs1 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 1).Select(z => z.Jobs).FirstOrDefault();
            ViewBag.Team_TextHeader = _unitOfWork.Team.GetAll().Where(z => z.Flag == 1).Select(z => z.TextHeader).FirstOrDefault();

            ViewBag.Team_Name2 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 2).Select(z => z.Name).FirstOrDefault();
            ViewBag.Team_Jobs2 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 2).Select(z => z.Jobs).FirstOrDefault();

            ViewBag.Team_Name3 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 3).Select(z => z.Name).FirstOrDefault();
            ViewBag.Team_Jobs3 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 3).Select(z => z.Jobs).FirstOrDefault();

            ViewBag.Team_Name4 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 4).Select(z => z.Name).FirstOrDefault();
            ViewBag.Team_Jobs4 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 4).Select(z => z.Jobs).FirstOrDefault();
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
        public IActionResult AboutUs()
        {
            ViewBag.TextVision = _unitOfWork.AboutUs.GetFirstOrDefault().Vision;
            ViewBag.TextMision = _unitOfWork.AboutUs.GetFirstOrDefault().Mission;
            return View();
        }

    }
}