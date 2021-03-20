using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using JetBrains.Annotations;

namespace Domain.Repositories.Users
{
    public interface IUserRepository
    {
        [CanBeNull]
        Task<User> GetUser([NotNull] string id);

        Task<bool> CheckLogin([NotNull] string login);

        [CanBeNull]
        Task<User> GetUserValidation([NotNull] string login);

        Task Create([NotNull] User user);

        [CanBeNull]
        IEnumerable<User> GetUsersByName([NotNull] string name);
    }
}