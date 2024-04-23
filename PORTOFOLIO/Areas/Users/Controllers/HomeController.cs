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
            ViewBag.TextSlide1_ = textSlide1 != null ? textSlide1.Substring(0, pageSize) + " . . ." : textSlide1;
            ViewBag.TextSlide1 = textSlide1;

            ViewBag.TitleSlide2 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "2").Select(z => z.Title).FirstOrDefault();
            string textSlide2 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "2").Select(z => z.Content).FirstOrDefault();
            ViewBag.TextSlide2_ = textSlide2 != null ? textSlide2.Substring(0, pageSize) + " . . ." : textSlide2;
            ViewBag.TextSlide2 = textSlide2;

            ViewBag.TitleSlide3 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "3").Select(z => z.Title).FirstOrDefault();
            string textSlide3 = _unitOfWork.Branda.GetAll().Where(z => z.Position == "3").Select(z => z.Content).FirstOrDefault();
            ViewBag.TextSlide3_ = textSlide3 != null ? textSlide3.Substring(0, pageSize) + " . . ." : textSlide3;
            ViewBag.TextSlide3 = textSlide3;

            #region About Us
            string Description = _unitOfWork.AboutUs.GetFirstOrDefault() != null ? _unitOfWork.AboutUs.GetFirstOrDefault().Description : null;
            if (Description != null)
            {
                ViewBag.Description = Description.Length > 100 ? Description.Substring(0, pageSize) + " . . ." : Description;
            }
            else
            {
                ViewBag.Description = "";
            }

            string TextVision = _unitOfWork.AboutUs.GetFirstOrDefault() != null ? _unitOfWork.AboutUs.GetFirstOrDefault().Vision : null;
            if (TextVision != null)
            {
                ViewBag.TextVision = TextVision.Length > 100 ? TextVision.Substring(0, pageSize) + " . . ." : TextVision;
            }
            else
            {
                ViewBag.TextVision = "";
            }
           
            string TextMision = _unitOfWork.AboutUs.GetFirstOrDefault() != null ? _unitOfWork.AboutUs.GetFirstOrDefault().Mission : null;
            if (TextMision != null)
            {
                ViewBag.TextMision = TextMision.Length > 100 ? TextMision.Substring(0, pageSize) + " . . ." : TextMision;
            }
            else
            {
                ViewBag.TextMision = "";
            }

            ViewBag.AboutUsDesc = _unitOfWork.AboutUs.GetFirstOrDefault() != null ? _unitOfWork.AboutUs.GetFirstOrDefault().Description : "";
            ViewBag.AboutUsVision = _unitOfWork.AboutUs.GetFirstOrDefault() != null ? _unitOfWork.AboutUs.GetFirstOrDefault().Vision : "";
            ViewBag.AboutUsMision = _unitOfWork.AboutUs.GetFirstOrDefault() != null ? _unitOfWork.AboutUs.GetFirstOrDefault().Mission : "";
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
            ViewBag.Portfolio_Img1_Left = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 1 && z.Position == "Left").Select(z => z.Photo).FirstOrDefault();
            ViewBag.Portfolio_Img1_Center = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 1 && z.Position == "Center").Select(z => z.Photo).FirstOrDefault();
            ViewBag.Portfolio_Img1_Right = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 1 && z.Position == "Right").Select(z => z.Photo).FirstOrDefault();

            ViewBag.Portfolio_Title2 = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 2).Select(z => z.Title).FirstOrDefault();
            ViewBag.Portfolio_Img2_Left = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 2 && z.Position == "Left").Select(z => z.Photo).FirstOrDefault();
            ViewBag.Portfolio_Img2_Center = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 2 && z.Position == "Center").Select(z => z.Photo).FirstOrDefault();
            ViewBag.Portfolio_Img2_Right = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 2 && z.Position == "Right").Select(z => z.Photo).FirstOrDefault();

            ViewBag.Portfolio_Title3 = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 3).Select(z => z.Title).FirstOrDefault();
            ViewBag.Portfolio_Img3_Left = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 3 && z.Position == "Left").Select(z => z.Photo).FirstOrDefault();
            ViewBag.Portfolio_Img3_Center = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 3 && z.Position == "Center").Select(z => z.Photo).FirstOrDefault();
            ViewBag.Portfolio_Img3_Right = _unitOfWork.Portofolio.GetAll().Where(z => z.Flag == 3 && z.Position == "Right").Select(z => z.Photo).FirstOrDefault();

            ViewBag.Portfolio_TextHeader = _unitOfWork.MsUDC.GetAll().Where(z => z.EntryKey == "Header" && z.Text1 == "MenuPortfolio").Select(x => x.Text3).FirstOrDefault();
            #endregion

            #region Team
            ViewBag.Team_Name1 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 1).Select(z => z.Name).FirstOrDefault();
            ViewBag.Team_Jobs1 = _unitOfWork.Team.GetAll().Where(z => z.Flag == 1).Select(z => z.Jobs).FirstOrDefault();
            ViewBag.Team_TextHeader = _unitOfWork.MsUDC.GetAll().Where(z => z.EntryKey == "Header" && z.Text1 == "MenuTeam").Select(x => x.Text3).FirstOrDefault();

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