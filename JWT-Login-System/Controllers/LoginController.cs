using App.Models;
using App.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {

        public readonly ILoginService _login;

        public LoginController(ILoginService login)
        {
            this._login = login;
        }
        
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(User user)
        {
            return Ok(new
            {
                data = this._login.AuthenticateAsync(user),
                success = true
            });   
        }        

    }
}
