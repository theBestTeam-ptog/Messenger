using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Domain.Models;
using Domain.Protos;
using Google.Protobuf.WellKnownTypes;
using Messenger.Pages;
using Messenger.Service;
using Messenger.ViewModels.Base;
using Chat = Domain.Models.Chat;
using Message = Domain.Models.Message;

namespace Messenger.ViewModels
{
    public class DialogViewModel : ViewModelBase
    {
        private ChatClient _chatClient;
        public string ChatName { get; set; }
        public ObservableCollection<Message> History { get; set; }
        public string ChatId { get; set; }

        public UserViewModel Teamate;
        
        public UserInfo[] UserInfos { get; set; }

        private string _messageContent;

        public string MessageContent
        {
            get => _messageContent;
            set => Set(ref _messageContent, value, nameof(MessageContent));
        }

        private RelayCommand _sendMessage;

        public RelayCommand SendMessage => _sendMessage ??= new RelayCommand( async x =>
        {
            if (_messageContent.Trim() != string.Empty)
            {
                var newMessage = new Message
                {
                    Time = DateTime.UtcNow,
                    AuthorId = MainPageViewModel.CurrentUser.Id,
                    AuthorName = MainPageViewModel.CurrentUser.UserName,
                    Content = _messageContent,
                };
                //_messages.Add(newMessage);
                await _chatClient.Client.SendMessageAsync(new MessageCreate
                {
                    Message = new Domain.Protos.Message
                    {
                        Time = Timestamp.FromDateTime(newMessage.Time),
                        AuthorId = newMessage.AuthorId.ToString(),
                        AuthorName = newMessage.AuthorName,
                        Content = newMessage.Content,
                    },
                    ChatId = ChatId,
                });
            }

            _messageContent = string.Empty;
        });

        public DialogViewModel(ChatClient chatClient, ObservableCollection<Message> messages, UserInfo[] userInfos,
            string chatName, string chatId)
        {
            _chatClient = chatClient;
            History = messages;
            UserInfos = userInfos;
            ChatName = chatName;
            ChatId = chatId;
        }
    }

    public sealed class UserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}