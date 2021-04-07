using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DbModels;
using Domain.Models;

namespace ChatService.Data
{
    public interface IUserRepository
    {
        Task<Messenger.ChatService.Protos.User> GetUser(string id);
        Task<bool> CheckLogin(string login);
        Task<Messenger.ChatService.Protos.User> GetUserValidation(string login);
        Task Create(UserDocument user);
        IEnumerable<Messenger.ChatService.Protos.User> GetUsersByName(string name);
        IEnumerable<Messenger.ChatService.Protos.User> Search(string suggest);
        Task<Messenger.ChatService.Protos.User> UserIsValid(string login, string password);
    }
}