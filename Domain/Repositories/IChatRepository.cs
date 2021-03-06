using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Domain.Protos;

namespace Domain.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> GetChatsAsync([NotNull] string userId);
        Task AddMessageAsync([NotNull] string chatId, [NotNull] Message message);
        Task CreateChatAsync([NotNull] Chat chat);
        Task<Chat> GetChatAsync(string chatId);
    }
}