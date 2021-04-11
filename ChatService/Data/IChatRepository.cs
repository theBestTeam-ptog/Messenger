using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DbModels;
using JetBrains.Annotations;
using Messenger.ChatService.Protos;

namespace ChatService.Data
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatDocument>> GetChatsAsync(string userId);
        Task<Chat> GetChatAsync([NotNull] string id);

        Task AddMessageAsync([NotNull] string chatId, [NotNull] Message message);

        Task CreateChatAsync([NotNull] Chat chat);
    }
}