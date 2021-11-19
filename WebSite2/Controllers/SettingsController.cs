using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebSite2.DB;
using WebSite2.Models;

namespace WebSite2.Controllers
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
            return Json(Settings.Get(login).Currency.Split(' ').First());
        }

        [HttpPost]
        public IActionResult Get(SettingsDto dto)
        {
            return Json(Settings.Get(dto.Login));
        }

        [HttpPost("save")]
        public IActionResult Save(SettingsDto dto)
        {
            Settings.Save(dto);
            return Ok();
        }
    }
}