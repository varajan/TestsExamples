using System.Net;
using System.Web.Mvc;
using WebSite.DB;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDto dto)
        {
            if (!dto.Email.IsValidEmail())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict, "Invalid email.");
            }

            if (Users.Emails.Contains(dto.Email.Trim()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict, "User with this email is already registered.");
            }

            if (Users.Names.Contains(dto.Login.ToLower().Trim()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict, "User with this login is already registered.");
            }

            if (dto.Password.Trim().Length < 5)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict, "Password is too short.");
            }

            Users.Add(dto);

            return Json("OK");
        }

        [HttpDelete]
        public ActionResult Delete(UserDto dto)
        {
            Users.Delete(dto.Login);

            return Json("OK");
        }

        [HttpGet]
        public ActionResult DeleteAllUsers() => DeleteAll();

        [HttpDelete]
        public ActionResult DeleteAll()
        {
            Users.Names.ForEach(Users.Delete);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}