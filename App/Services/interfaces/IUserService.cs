using App.Models;
using System.Collections.Generic;

namespace App.Services.interfaces
{
    public interface IUserService
    {
        User GetUserByUsername(string username);
        IEnumerable<User> GetAllUsers();
    }
}
