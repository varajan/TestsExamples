using Microsoft.AspNetCore.Mvc;
using WebSite2.DB;
using WebSite2.Models;

namespace WebSite2.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost("Register")]
        public IActionResult Register(UserDto dto)
        {
            if (!dto.Email.IsValidEmail())
            {
                return Conflict("Invalid email.");
            }

            if (Users.Emails.Contains(dto.Email.Trim()))
            {
                return Conflict("User with this email is already registered.");
            }

            if (Users.Names.Contains(dto.Login.ToLower().Trim()))
            {
                return Conflict("User with this login is already registered.");
            }

            if (dto.Password.Trim().Length < 5)
            {
                return Conflict("Password is too short.");
            }

            Users.Add(dto);

            return Json("OK");
        }

        [HttpDelete("delete")]
        public IActionResult Delete(UserDto dto)
        {
            Users.Delete(dto.Login);

            return Json("OK");
        }

        [HttpGet("deleteAll")]
        public IActionResult DeleteAllUsers() => DeleteAll();

        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll()
        {
            Users.Names.ForEach(Users.Delete);

            return Ok();
        }
    }
}