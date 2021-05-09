using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using Domain.Protos;
using Google.Protobuf.WellKnownTypes;
using JetBrains.Annotations;
using Messenger.Service;
using Messenger.ViewModels;
using Message = Domain.Models.Message;

namespace Messenger.Pages
{
    [UsedImplicitly]
    public partial class Dialog : Page
    {
        private readonly ObservableCollection<Message> _messages;
        private readonly string _uid;
        private readonly ChatClient _client;

        public Dialog(ObservableCollection<Message> messages, string uid)
        {
            _messages = messages;
            _uid = uid;
            InitializeComponent();
            _client = Bootstrapper.Container.GetInstance<ChatClient>();

            messagesList.ItemsSource = _messages;
        }

        private async void SendMessage(object sender, MouseButtonEventArgs e)
        {
            if (message.Text.Trim() != string.Empty)
            {
                var newMessage = new Message
                {
                    Time = DateTime.UtcNow,
                    AuthorId = MainPageViewModel.CurrentUser.Id,
                    AuthorName = MainPageViewModel.CurrentUser.UserName,
                    Content = message.Text,
                };
                _messages.Add(newMessage);
                _client.Client.SendMessage(new MessageCreate
                {
                    Message = new Domain.Protos.Message
                    {
                        Time = Timestamp.FromDateTime(newMessage.Time),
                        AuthorId = newMessage.AuthorId.ToString(),
                        AuthorName = newMessage.AuthorName,
                        Content = newMessage.Content,
                    },
                    ChatId = _uid,
                });
            }

            message.Text = string.Empty;
        }

        private void LinkDocument(object sender, MouseButtonEventArgs e)
        {
        }
    }
}