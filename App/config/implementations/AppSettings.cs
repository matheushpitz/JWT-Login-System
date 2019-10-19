using config.interfaces;
using Microsoft.Extensions.Configuration;

namespace config.implementations
{
    public class AppSettings : IAppSettings
    {

        private readonly string JwtSecurityKey;

        public AppSettings(IConfiguration configuration)
        {
            this.JwtSecurityKey = configuration.GetSection("JWT:securityKey").Value;
        }

        public string GetJwtSecurityKey()
        {
            return this.JwtSecurityKey;
        }
    }
}
