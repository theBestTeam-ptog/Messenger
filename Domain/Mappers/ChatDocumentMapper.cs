using Domain.DbModels;
using Domain.Models;
using JetBrains.Annotations;

namespace Domain.Mappers
{
    public sealed class ChatDocumentMapper : IMapper<Chat, ChatDocument>, IMapper<ChatDocument, Chat>
    {
        [CanBeNull]
        public ChatDocument Map([CanBeNull] Chat source) => source is null 
            ? null 
            : new ChatDocument
            {
                Id = source.Id,
                UserIds = source.UserIds,
                History = source.History,
            };

        [CanBeNull]
        public Chat Map([CanBeNull] ChatDocument source) => source is null 
            ? null 
            : new Chat
            {
                Id = source.Id,
                UserIds = source.UserIds,
                History = source.History,
            };
    }
}