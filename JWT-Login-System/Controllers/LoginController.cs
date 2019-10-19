using App.config;
using App.Models;
using App.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                return Ok(new
                {
                    token = this._login.Authenticate(user),
                    success = true
                });
            } catch(Exception e) {
                return Ok(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }

        // Administrator and Users can use this GET.
        [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
        [HttpGet("GetUserData")]
        public IActionResult GetUserData() {
            try
            {
                return Ok(new
                {
                    success = true,
                    data = this._user.GetUserByUsername(HttpContext.User.Identity.Name)
                });
            } catch(Exception e)
            {
                return Ok(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }

        // Only Administrators.
        [Authorize(Roles = Roles.ADMIN)]
        [HttpGet("GetAllUserData")]
        public IActionResult GetAllUserData() {
            try
            {
                return Ok(new
                {
                    success = true,
                    data = this._user.GetAllUsers()
                });
            } catch(Exception e)
            {
                return Ok(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }

    }
}
