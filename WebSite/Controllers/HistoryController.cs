using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class HistoryController : Controller
    {
        private readonly string history = $"{Path.GetTempPath()}/history.data";
        private readonly string settings = $"{Path.GetTempPath()}/settings.data";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = System.IO.File.Exists(history)
                ? System.IO.File.ReadAllLines(history).Reverse().Take(9).Select(x => x.Split(';').ToList()).ToList()
                : new List<List<string>>();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Clear()
        {
            System.IO.File.Delete(history);

            return Json("OK");
        }

        [HttpPost]
        public ActionResult Save(string amount, string percent, string interest, int days, string endDate, int year, string income)
        {
            var format = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[0]
                : "dd/MM/yyyy";
            var startDate = DateTime.ParseExact(endDate, format, CultureInfo.InvariantCulture).AddDays(-days).ToString(format, CultureInfo.InvariantCulture);

            System.IO.File.AppendAllText(history, $"{amount}; {percent}%; {days}; {year}; {startDate}; {endDate}; {interest}; {income}{Environment.NewLine}");
            return Json("OK");
        }
    }
}