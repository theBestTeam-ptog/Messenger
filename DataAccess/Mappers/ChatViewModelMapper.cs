using System;
using Core.IoC;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;
using System.Linq;

namespace DataAccess.Mappers
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatViewModelMapper : IMapper<Domain.Protos.Chat, ChatViewModel>
    {
        [NotNull]
        public ChatViewModel Map(Domain.Protos.Chat source) =>
            new ChatViewModel
            {
                ChatName = source.History.Select(HelpChat).Last().Content,
                History = source.History.Select(HelpChat).ToList(),
                ChatId = source.Id
            };

        private static Message HelpChat(Domain.Protos.Message message) =>
            new Message
            {
                AuthorId = Guid.Parse((ReadOnlySpan<char>) message.AuthorId),
                Content = message.Content,
                Time = message.Time.ToDateTime()
            };
    }
}