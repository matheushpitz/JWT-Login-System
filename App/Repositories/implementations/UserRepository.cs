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
            return this._users.Select(u => u.GetSafeInstance());
        }

        public User GetUserByCredentials(User user)
        {
            IEnumerable<User> users = this._users.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password));
            if(users.Count() > 0)
                return users.FirstOrDefault().GetSafeInstance();

            return null;
        }

        public User GetUserByUsername(string username)
        {
            IEnumerable<User> users = this._users.Where(u => u.Username.Equals(username));
            if(users.Count() > 0)
                return users.FirstOrDefault().GetSafeInstance();

            return null;
        }        
    }
}
