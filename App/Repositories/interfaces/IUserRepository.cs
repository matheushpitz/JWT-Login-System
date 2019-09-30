
using App.Models;
using System.Threading.Tasks;

namespace App.Repositories.interfaces
{
    public interface IUserRepository
    {
        User GetUserByCredentialsAsync(User user);
    }
}
