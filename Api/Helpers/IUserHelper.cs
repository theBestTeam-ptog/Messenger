using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DbModels;
using Domain.Models;
using JetBrains.Annotations;

namespace Api.Helpers
{
    public interface IUserHelper
    {
        Task<Tuple<bool, User>> UserIsValid([CanBeNull] User userInfo);

        Task RegisterUser([CanBeNull] User user);

        [CanBeNull]
        IEnumerable<User> FindUsers([NotNull] string name);
        
        IEnumerable<UserViewModel> Search([NotNull] string suggest);
    }
}