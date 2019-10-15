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

        public string AuthenticateAsync(User user)
        {

            string jwtToken = null;

            User data = this._repository.GetUserByCredentialsAsync(user);

            if(data != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this._settings.GetJWTKey());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, data.Name),
                    new Claim(ClaimTypes.Role, data.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                jwtToken = tokenHandler.WriteToken(token);

            }

            return jwtToken;
        }        
    }
}
