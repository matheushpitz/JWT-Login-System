using System.Collections.Generic;
using App.Models;
using App.Repositories.interfaces;
using App.Services.interfaces;

namespace App.Services.implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this._repository.GetAllUsers();
        }

        public User GetUserByUsername(string username)
        {
            return this._repository.GetUserByUsername(username);
        }
    }
}
