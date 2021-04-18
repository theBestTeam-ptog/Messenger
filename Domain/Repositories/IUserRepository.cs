using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.ChatService.Protos;
using JetBrains.Annotations;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync([NotNull] string id);
        Task<bool> CheckLoginAsync([NotNull] string login);
        Task<User> GetUserValidationAsync([NotNull] string login, [NotNull] string password);
        Task CreateUserAsync([NotNull] User user);
        IEnumerable<User> GetUsersByNameAsync([NotNull] string name);
        IEnumerable<User> SearchSync([NotNull] string suggest);
    }
}