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


        [HttpPost]
        public ActionResult Save(UserDto dto)
        {
            Users.Add(dto.Login, dto.Password);

            return Json("OK");
        }

        [HttpDelete]
        public ActionResult Delete(UserDto dto)
        {
            Users.Delete(dto.Login);

            return Json("OK");
        }

        [HttpDelete]
        public ActionResult DeleteAll()
        {
            Users.Names.ForEach(Users.Delete);

            return Json("OK");
        }
    }
}