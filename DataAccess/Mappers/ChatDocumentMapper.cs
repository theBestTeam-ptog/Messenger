using Core.IoC;
using DataAccess.DbModels;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;

namespace DataAccess.Mappers
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatDocumentMapper : IDuplexMapper<Chat, ChatDocument>
    {
        public ChatDocument Map(Chat source) => source is null 
            ? null 
            : new ChatDocument
            {
                Id = source.Id,
                UserIds = source.UserIds,
                History = source.History,
            };
    
        public Chat Map(ChatDocument source) => source is null 
            ? null 
            : new Chat
            {
                Id = source.Id,
                UserIds = source.UserIds,
                History = source.History,
            };
    }
}