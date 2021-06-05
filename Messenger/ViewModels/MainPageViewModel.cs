using System;
using System.Collections.ObjectModel;
using System.Linq;
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
using User = Domain.Protos.User;

namespace Messenger.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public static UserViewModel CurrentUser;
        public UserViewModel Teamate { get; set; } = new UserViewModel();

        private CancellationTokenSource _cancellationTs;
        private readonly ChatClient _client;

        public MainPageViewModel()
        {
            _client = Bootstrapper.Container.GetInstance<ChatClient>();
            CurrentUser.Chats.ForEach(x => { ChatsViewModels.Add(x); });
        }

        private ObservableCollection<ChatViewModel> _chatsViewModels = new ObservableCollection<ChatViewModel>();

        public ObservableCollection<ChatViewModel> ChatsViewModels
        {
            get => _chatsViewModels;
            set => Set(ref _chatsViewModels, value);
        }

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value);
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
            var frame = x as Frame;
            Messages.Clear();

            var selectedChat = CurrentUser.Chats.Find(c => c.ChatId == SelectedDialog.ChatId);
            Teamate.UserName = selectedChat?.UserInfos
                .First(u => Guid.Parse(u.Id) != CurrentUser.Id)
                .UserName;
            
            await LoadChatAsync();
            frame?.Navigate(new Dialog(Messages, SelectedDialog.ChatId, Teamate));
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
                await foreach (var message in reply.ResponseStream.ReadAllAsync())
                {
                    Messages.Add(new Message
                    {
                        AuthorId = Guid.Parse(message.AuthorId),
                        Content = message.Content,
                        Time = message.Time.ToDateTime(),
                        AuthorName = message.AuthorName
                    });
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            { }
        }
    }
}