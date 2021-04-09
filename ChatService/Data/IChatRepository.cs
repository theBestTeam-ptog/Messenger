using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DbModels;
using JetBrains.Annotations;
using Messenger.ChatService.Protos;

namespace ChatService.Data
{
    public interface IChatRepository
    {
        Task<List<ChatDocument>> GetChats(IEnumerable<string> ids);
        Task<Chat> GetChat([NotNull] string id);

        Task AddMessage([NotNull] string chatId, [NotNull] Message message);

        Task Create([NotNull] Chat chat);
    }
}