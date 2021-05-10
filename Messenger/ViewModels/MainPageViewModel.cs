using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoMapper.Internal;
using Domain.Models;
using Domain.Protos;
using Grpc.Core;
using Messenger.Pages;
using Messenger.Service;
using Messenger.ViewModels.Base;
using Message = Domain.Models.Message;

namespace Messenger.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public static UserViewModel CurrentUser;

        private CancellationTokenSource _cancellationTs;
        private ObservableCollection<ChatViewModel> _chatsViewModels = new ObservableCollection<ChatViewModel>();
        private readonly ChatClient _client;

        public MainPageViewModel()
        {
            _client = Bootstrapper.Container.GetInstance<ChatClient>();
            CurrentUser.Chats.ForEach(x => { ChatsViewModels.Add(x); });
        }

        public ObservableCollection<ChatViewModel> ChatsViewModels
        {
            get => _chatsViewModels;
            set => Set(ref _chatsViewModels, value);
        }

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>()
        {
            new Message
                {Content = "123", AuthorId = Guid.Parse("664ee829-4d3e-4f07-a88f-9406c91f85ee")},
            new Message
                {Content = "123", AuthorId = Guid.Parse("4a98ac74-b5ea-4e5d-ae74-aa6cd1667204")},
            new Message
                {Content = "123", AuthorId = Guid.Parse("664ee829-4d3e-4f07-a88f-9406c91f85ee")},
        };

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                NotifyPropertyChanged(nameof(Messages));
            }
        }

        private ChatViewModel _selectedDialog;
        public ChatViewModel SelectedDialog
        {
            get => _selectedDialog;
            set => Set(ref _selectedDialog, value);
        }

        private RelayCommand _openDialog;
        public RelayCommand OpenDialog => new RelayCommand(async x =>
        {
            Messages = CurrentUser?.Chats?
                .Find(x => x.ChatId == SelectedDialog.ChatId)?
                .History ?? new ObservableCollection<Message>();

            var frame = x as Frame;
            frame?.Navigate(new Dialog(Messages, SelectedDialog.ChatId));
            // await LoadChatAsync();
        });

        private async Task LoadChatAsync()
        {
            _cancellationTs?.Cancel();
            _cancellationTs = new CancellationTokenSource();

            var reply = _client.Client.JoinChat(new GetChat()
            {
                ChatId = SelectedDialog.ChatId
            }, cancellationToken: _cancellationTs.Token);
            try
            {
                var messages = new ObservableCollection<Message>();

                await foreach (var message in reply.ResponseStream.ReadAllAsync())
                {
                    messages.Add(new Message
                    {
                        AuthorId = Guid.Parse(message.AuthorId),
                        Content = message.Content,
                        Time = message.Time.ToDateTime()
                    });
                }

                if (Messages.Count == 0)
                    Messages = messages;
                else
                    messages.ForAll(x => Messages.Add(x));
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
            }
        }
    }
}