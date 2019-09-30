using config.interfaces;
using Microsoft.Extensions.Configuration;

namespace config.implementations
{
    public class AppSettings : IAppSettings
    {

        public readonly string JWTKey;

        public AppSettings(IConfiguration configuration)
        {
            this.JWTKey = configuration.GetSection("jwt:key").Value;
        }

        public string GetJWTKey()
        {
            return this.JWTKey;
        }
    }
}
