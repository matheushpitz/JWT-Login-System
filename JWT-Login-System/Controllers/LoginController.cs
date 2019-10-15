using App.config;
using App.Models;
using App.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {

        public readonly ILoginService _login;
        public readonly IUserService _user;

        public LoginController(ILoginService login, IUserService user)
        {
            this._login = login;
            this._user = user;
        }
        
        // Everybody can use it.
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(User user)
        {
            var a = HttpContext.User;
            return Ok(new
            {
                token = this._login.AuthenticateAsync(user),
                success = true
            });   
        }

        // Administrator and Users can use this GET.
        [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
        [HttpGet("GetUserData")]
        public IActionResult GetUserData() {
            return Ok(new {
                success = true,
                data = this._user.GetUserByUsername(HttpContext.User.Identity.Name)
            });
        }

        // Only Administrators.
        [Authorize(Roles = Roles.ADMIN)]
        [HttpGet("GetAllUserData")]
        public IActionResult GetAllUserData() {
            return Ok(new {
                success = true,
                data = this._user.GetAllUsers()
            });
        }

    }
}
