using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DbModels;
using Messenger.ChatService.Protos;

namespace ChatService.Data
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string id);
        Task<bool> CheckLoginAsync(string login);
        Task<User> GetUserValidationAsync(string login, string password);
        Task CreateUserAsync(User user);
        IEnumerable<User> GetUsersByNameAsync(string name);
        IEnumerable<User> SearchAsync(string suggest);
    }
}