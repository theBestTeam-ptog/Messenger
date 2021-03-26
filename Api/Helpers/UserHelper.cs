using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DbModels;
using Domain.Models;
using Domain.Repositories.Users;
using JetBrains.Annotations;

namespace Api.Helpers
{
    [UsedImplicitly]
    public sealed class UserHelper : IUserHelper
    {
        private readonly IUserRepository _db;

        public UserHelper(IUserRepository db)
        {
            _db = db;
        }

        public async Task<Tuple<bool, User>> UserIsValid(User userInfo)
        {
            if (userInfo?.Login is null) return null;
            var user = await _db.GetUserValidation(userInfo.Login);
            return Tuple.Create(user != null && user.Password.Equals(userInfo.Password), user);
        }

        public async Task RegisterUser(User user)
        {
            if (user is null) return;
            await _db.Create(user);
        }
        
        public IEnumerable<User> FindUsers(string name) => _db.GetUsersByName(name);
        
        public IEnumerable<UserViewModel> Search(string suggest) => _db.Search(suggest);
    } 
}
