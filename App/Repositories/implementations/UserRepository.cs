using App.config;
using App.Models;
using App.Repositories.interfaces;
using System.Collections.Generic;
using System.Linq;

namespace App.Repositories.implementations
{
    public class UserRepository : IUserRepository
    {

        private User[] _users;

        public UserRepository()
        {
            this._users = new User[] {
                new User{ Username = "admin", Password = "admin", Name = "Administrator", Role = Roles.ADMIN},
                new User{ Username = "defaultUser", Password = "123456", Name = "Default User", Role = Roles.USER},
            };
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this._users;
        }

        public User GetUserByCredentialsAsync(User user)
        {
            return this._users.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password)).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return this._users.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }        
    }
}
