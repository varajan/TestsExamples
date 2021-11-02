using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using WebSite.DB;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HistoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(SaveHistoryDto dto)
        {
            var history = History.Get(dto.Login);
            history.Reverse();

            var result = history
                .Take(9)
                .Select(x =>
                new[]
                {
                    x.Amount.AsDecimal().FormatNumber(x.Login),
                    x.Percent + "%",
                    x.Days.ToString(),
                    x.Year,
                    x.StartDate.FormatDate(dto.Login),
                    x.EndDate.FormatDate(dto.Login),
                    x.Interest.AsDecimal().FormatNumber(dto.Login),
                    x.Income.AsDecimal().FormatNumber(dto.Login)
                });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Clear(SaveHistoryDto dto)
        {
            History.Clear(dto.Login);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult Save(SaveHistoryDto dto)
        {
            History.Add(dto);
            return Json("OK");
        }
    }
}