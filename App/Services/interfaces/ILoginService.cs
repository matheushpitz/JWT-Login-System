using App.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.interfaces
{
    public interface ILoginService
    {
        string Authenticate(User user);        
        string Reauthenticate(HttpContext context);        
    }
}
