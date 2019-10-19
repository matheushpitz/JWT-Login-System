
using App.Models;
using System.Collections.Generic;

namespace App.Repositories.interfaces
{
    public interface IUserRepository
    {
        User GetUserByCredentials(User user);
        User GetUserByUsername(string username);
        IEnumerable<User> GetAllUsers();
    }
}
