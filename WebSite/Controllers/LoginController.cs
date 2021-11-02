using System.Web.Mvc;
using WebSite.DB;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class LoginController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public ActionResult Validate(UserDto dto)
        {
            if (Users.IsValid(dto.Login, dto.Password))
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound("Invalid credentials");
        }
    }
}