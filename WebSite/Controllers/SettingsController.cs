using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class SettingsController : Controller
    {
        private readonly string settings = $"{Path.GetTempPath()}/settings.data";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Date(string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var format = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[0]
                : "dd/MM/yyyy";

            return Json(dateTime.ToString(format, CultureInfo.InvariantCulture), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Number(decimal number)
        {
            var result = Math.Round(number, 2, MidpointRounding.AwayFromZero)
                .ToString("N", CultureInfo.InvariantCulture);
            var format = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[1]
                : "123,456,789.00";

            switch (format)
            {
                case "123.456.789,00":
                    result = result.Replace(',', ' ').Replace('.', ',').Replace(' ', '.');
                    break;

                case "123 456 789.00":
                    result = result.Replace(',', ' ');
                    break;

                case "123 456 789,00":
                    result = result.Replace(',', ' ').Replace('.', ',');
                    break;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Currency()
        {
            var result = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[2]
                : "$ - US dollar";

            return Json(result.Split(' ').First(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = new Settings
            {
                DateFormat = "dd/MMM/yyyy",
                NumberFormat = "123,456,789.00",
                Currency = "$ - US dollar"
            };

            if (System.IO.File.Exists(settings))
            {
                var values = System.IO.File.ReadAllText(settings).Split(';');

                result.DateFormat = values[0];
                result.NumberFormat = values[1];
                result.Currency = values[2];
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(string dateFormat, string numberFormat, string currency)
        {
            System.IO.File.WriteAllText(settings, $"{dateFormat};{numberFormat};{currency}");
            return Json("OK");
        }

        class Settings
        {
            public string DateFormat { get; set; }
            public string NumberFormat { get; set; }
            public string Currency { get; set; }
        }
    }
}