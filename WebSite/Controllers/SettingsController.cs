using System.Linq;
using System.Web.Mvc;
using WebSite.DB;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Date(string date, string login)
        {
            return Json(date.FormatDate(login), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Number(decimal number, string login)
        {
            return Json(number.FormatNumber(login), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Currency(string login)
        {
            return Json(Settings.Get(login).Currency.Split(' ').First(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Get(SettingsDto dto)
        {
            return Json(Settings.Get(dto.Login));
        }

        [HttpPost]
        public ActionResult Save(SettingsDto dto)
        {
            Settings.Save(dto);
            return Json("OK");
        }
    }
}