using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Protos;
using JetBrains.Annotations;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync([NotNull] string id);
        Task<bool> CheckLoginAsync([NotNull] string login);
        Task<User> GetUserValidationAsync([NotNull] string login, [NotNull] string password);
        Task CreateUserAsync([NotNull] string userName, string login, string password);
        IEnumerable<User> GetUsersByNameAsync([NotNull] string name);
        IEnumerable<User> SearchSync([NotNull] string suggest);
    }
}