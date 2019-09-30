using System.Threading.Tasks;
using App.Models;
using App.Repositories.interfaces;

namespace App.Repositories.implementations
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByCredentialsAsync(User user)
        {
            // Here should be used the database to retrieve the data.
            if(user.Username.Equals("admin") && user.Password.Equals("admin"))
            {
                return new User
                {
                    Username = "admin",
                    Password = "admin"
                };
            } else if(user.Username.Equals("defaultUser") && user.Password.Equals("123456"))
            {
                return new User
                {
                    Username = "defaultUser",
                    Password = "123456"
                };
            }

            return null;
        }
    }
}
