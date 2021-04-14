using System;
using System.Linq;
using Core.IoC;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;
using Chat = Messenger.ChatService.Protos.Chat;

namespace DataAccess.Mappers
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatViewModelMapper : IMapper<Chat, ChatViewModel>
    {
        [NotNull]
        public ChatViewModel Map(Chat source) =>
            new ChatViewModel
            {
                ChatName = source.History.Select(HelpChat).Last().Content,
                History = source.History.Select(HelpChat).ToList(),
                ChatId = source.Id
            };

        private static Message HelpChat(Messenger.ChatService.Protos.Message message) =>
            new Message
            {
                AuthorId = Guid.Parse((ReadOnlySpan<char>) message.AuthorId),
                Content = message.Content,
                Time = message.Time.ToDateTime()
            };
    }
}