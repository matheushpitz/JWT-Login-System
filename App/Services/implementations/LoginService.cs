using App.Models;
using App.Repositories.interfaces;
using App.Services.interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using config.interfaces;

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
                // Create Token Handler
                var tokenHandler = new JwtSecurityTokenHandler();
                // Get Jwt Security Key.
                var key = Encoding.ASCII.GetBytes(this._settings.GetJwtSecurityKey());
                // Create Token Descriptor.
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim(ClaimTypes.Role, data.Role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                // Create the Token as JSON.
                var token = tokenHandler.CreateToken(tokenDescriptor);
                // Serialize the JSON.
                jwtToken = tokenHandler.WriteToken(token);

            }

            return jwtToken;
        }        
    }
}
