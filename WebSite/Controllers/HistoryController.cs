using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebSite.DB;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Route("api/[controller]")]
    public class HistoryController : Controller
    {
        [HttpGet]
        public IActionResult Get(SaveHistoryDto dto)
        {
            var history = History.Get(dto.Login);
            history.Reverse();

            var result = history
                .Take(9)
                .Select(x =>
                new[]
                {
                    Convert.ToDecimal(x.Amount).FormatNumber(x.Login),
                    x.Percent + "%",
                    x.Days.ToString(),
                    x.Year,
                    x.StartDate.FormatDate(dto.Login),
                    x.EndDate.FormatDate(dto.Login),
                    Convert.ToDecimal(x.Interest).FormatNumber(dto.Login),
                    Convert.ToDecimal(x.Income).FormatNumber(dto.Login)
                });

            return Json(result);
        }

        [HttpPost("clear")]
        public IActionResult Clear(SaveHistoryDto dto)
        {
            History.Clear(dto.Login);
            return Ok();
        }

        [HttpPost("save")]
        public ActionResult Save(SaveHistoryDto dto)
        {
            History.Add(dto);
            return Ok();
        }
    }
}