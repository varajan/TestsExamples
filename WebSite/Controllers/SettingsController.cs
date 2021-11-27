using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebSite.DB;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        [HttpGet("date")]
        public IActionResult Date(string date, string login)
        {
            return Json(date.FormatDate(login));
        }

        [HttpGet("number")]
        public IActionResult Number(decimal number, string login)
        {
            return Json(number.FormatNumber(login));
        }

        [HttpGet("currency")]
        public IActionResult Currency(string login)
        {
            var currency = Constants.Get("currency").ElementAt(Settings.Get(login).Currency);

            return Json(currency.Split(' ').First());
        }

        [HttpPost]
        public IActionResult Get([FromBody] SettingsDto dto)
        {
            return Json(Settings.Get(dto.Login));
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] SettingsDto dto)
        {
            Settings.Save(dto);
            return Ok();
        }

        [HttpGet("values")]
        public IActionResult GetValues(string name)
        {
            return Json(Constants.Get(name));
        }

        [HttpGet("days")]
        public IActionResult GetDays()
        {
            var today = DateTime.Today;
            var result = Enumerable.Range(1, DateTime.DaysInMonth(today.Year, today.Month)).ToList();
            return Json(result);
        }

        [HttpGet("years")]
        public IActionResult GetYears()
        {
            var result = Enumerable.Range(2010, 20).ToList();
            return Json(result);
        }
    }
}