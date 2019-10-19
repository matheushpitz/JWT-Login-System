using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.interfaces
{
    public interface ILoginService
    {
        string Authenticate(User user);        
    }
}
