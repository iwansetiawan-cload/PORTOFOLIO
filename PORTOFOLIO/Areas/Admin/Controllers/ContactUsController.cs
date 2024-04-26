using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTOFOLIO.Utility;
using System.Data;

namespace PORTOFOLIO.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            //tesss
            return View();
        }
    }
}
