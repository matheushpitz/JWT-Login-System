using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config.interfaces
{
    public interface IAppSettings
    {
        string GetJWTKey();
    }
}
