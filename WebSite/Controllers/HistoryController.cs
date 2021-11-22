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
        public IActionResult Get(string login)
        {
            var history = History.Get(login);
            history.Reverse();

            var result = history
                .Take(9)
                .Select(x =>
                new[]
                {
                    x.Amount.ToDecimal().FormatNumber(x.Login),
                    x.Percent + "%",
                    x.Days.ToString(),
                    x.Year,
                    x.StartDate.FormatDate(login),
                    x.EndDate.FormatDate(login),
                    x.Interest.ToDecimal().FormatNumber(x.Login),
                    x.Income.ToDecimal().FormatNumber(x.Login)
                });

            return Json(result);
        }

        [HttpPost("clear")]
        public IActionResult Clear([FromBody] SaveHistoryDto dto)
        {
            History.Clear(dto.Login);
            return Ok();
        }

        [HttpPost("save")]
        public ActionResult Save([FromBody] SaveHistoryDto dto)
        {
            History.Add(dto);
            return Ok();
        }
    }
}