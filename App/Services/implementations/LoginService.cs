using App.Models;
using App.Repositories.interfaces;
using App.Services.interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using config.interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace App.Services.implementations
{
    public class LoginService : ILoginService
    {

        private readonly IUserRepository _repository;
        private readonly IAppSettings _settings;

        public LoginService(IUserRepository repository, IAppSettings settings)
        {
            this._repository = repository;
            this._settings = settings;
        }

        public string Authenticate(User user)
        {

            string jwtToken = null;

            User data = this._repository.GetUserByCredentials(user);

            if(data != null)
            {
                jwtToken = this.generateJwt(data.Username, data.Role);

            }

            return jwtToken;
        }

        public string Reauthenticate(HttpContext context)
        {
            return this.generateJwt(context.User.Identity.Name, context.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value);
        }

        private string generateJwt(string username, string role) {
            // Create Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            // Get Jwt Security Key.
            var key = Encoding.ASCII.GetBytes(this._settings.GetJwtSecurityKey());
            // Create Token Descriptor.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            // Create the Token as JSON.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Serialize the JSON.
            return tokenHandler.WriteToken(token);
        }
    }
}
