using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using Domain.Models;
using Domain.Protos;
using Google.Protobuf.WellKnownTypes;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;
using Messenger.Service;
using Messenger.ViewModels;
using Messenger.Views;
using Message = Domain.Models.Message;

namespace Messenger.Pages
{
    [UsedImplicitly]
    public partial class Dialog : Page
    {
        private readonly ObservableCollection<Message> _messages;
        private readonly string _uid;
        private readonly ChatClient _client;
        
        public Dialog(ObservableCollection<Message> messages, string uid, UserViewModel teamate)
        {
            _messages = messages;
            _uid = uid;
            InitializeComponent();
            _client = Bootstrapper.Container.GetInstance<ChatClient>();
            InfoBar.Text = teamate.UserName;
            messagesList.ItemsSource = _messages;
            ScrollViewer.ScrollToBottom();
        }

        private async void SendMessage_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SendMessage();
        }

        private void LinkDocument(object sender, MouseButtonEventArgs e)
        { }

        private void MessagesScroll_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            var icon = (PackIcon)sender;
            icon.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            var icon = (PackIcon) sender;
            icon.Foreground = new SolidColorBrush(Colors.White);
        }

        private void SendMessage_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                SendMessage();
        }

        private async void SendMessage()
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
                ScrollViewer.ScrollToBottom();
            }

            message.Text = string.Empty;
        }
    }
}