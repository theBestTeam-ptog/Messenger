using System;
using System.Linq;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;
using Chat = Messenger.ChatService.Protos.Chat;

namespace DataAccess.Mappers
{
    public class ChatViewModelMapper : IMapper<Chat, ChatViewModel>
    {
        [NotNull]
        public ChatViewModel Map(Chat source) =>
            new ChatViewModel
            {
                ChatName = source.UserIds.Last(),
                History = source.History.Select(HelpChat).ToList()
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