using System;
using System.Collections.ObjectModel;
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
        public ChatViewModel Map(Domain.Protos.Chat source) => source == null
            ? null
            : new ChatViewModel
            {
                ChatName = source.History.Select(HelpChat).Last().Content,
                History = new ObservableCollection<Message>(source.History.Select(HelpChat)),
                ChatId = source.Id,
                UserInfos = source.UserInfos.Select(Map).ToArray(),
            };

        private static Message HelpChat(Domain.Protos.Message message) =>
            new Message
            {
                AuthorId = Guid.Parse((ReadOnlySpan<char>) message.AuthorId),
                Content = message.Content,
                Time = message.Time.ToDateTime(),
                AuthorName = message.AuthorName,
            };

        private static UserInfo Map(Domain.Protos.UserInfo s) => s == null
            ? null
            : new UserInfo
            {
                Id = s.Id,
                UserName = s.UserName,
            };
    }
}