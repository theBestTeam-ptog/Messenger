using System.Threading.Tasks;
using Domain.Models;
using JetBrains.Annotations;

namespace Domain.Repositories.Chats
{
    public interface IChatRepository
    {
        [CanBeNull]
        Task<Chat> Get([NotNull] string id);

        Task AddMessage([NotNull] string chatId, [NotNull] Message message);

        Task Create([NotNull] Chat chat);
    }
}