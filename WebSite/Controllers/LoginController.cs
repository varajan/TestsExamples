using System.Net;
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

        [HttpPost]
        public ActionResult Remind(string email)
        {
            email = email.ToLower();

            if (!email.IsValidEmail())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict, "Invalid email.");
            }

            if (!Users.Emails.Contains(email))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict, "No user was found.");
            }

            return Json($"Email with instructions was sent to {email}");
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