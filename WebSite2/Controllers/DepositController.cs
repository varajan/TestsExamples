using Microsoft.AspNetCore.Mvc;

namespace WebSite2.Controllers
{
    [Route("api/[controller]")]
    public class DepositController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
