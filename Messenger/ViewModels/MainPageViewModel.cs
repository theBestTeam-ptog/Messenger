using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Domain.Models;
using Domain.Protos;
using Grpc.Core;
using Messenger.Service;
using Messenger.Utils;
using Messenger.ViewModels.Base;

namespace Messenger.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public static UserViewModel CurrentUser;
        private CancellationTokenSource _cancellationTS;

        private ObservableCollection<ChatViewModel> _chatsViewModels = new ObservableCollection<ChatViewModel>();
        public ObservableCollection<ChatViewModel> ChatsViewModels
        {
            get => _chatsViewModels;
            set => Set(ref _chatsViewModels, value);
        }

        private ObservableCollection<Domain.Models.Message> _messages =
            new ObservableCollection<Domain.Models.Message>();

        public ObservableCollection<Domain.Models.Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                NotifyPropertyChanged(nameof(Messages));
            } 
        }

        public MainPageViewModel()
        {
            CurrentUser.Chats.ForEach(x =>
            {
                ChatsViewModels.Add(x);
            });
        }

        private ChatViewModel _selectedDialog;
        public ChatViewModel SelectedDialog
        {
            get => _selectedDialog;
            set => Set(ref _selectedDialog, value);
        }

        private RelayCommand _openDialog;
        public RelayCommand OpenDialog => _openDialog = new RelayCommand(async x =>
        {
            var frame = x as Frame;
            frame.Navigate(new Uri(Constants.PagesUris.Dialog, UriKind.Relative));
            
            await LoadChatAsync();
        });

        private async Task LoadChatAsync()
        {
            var client = Bootstrapper.Container.GetInstance<ChatClient>();
            
            _cancellationTS?.Cancel();
            _cancellationTS = new CancellationTokenSource();
            
            var reply = client.Client.JoinChat(new GetChat()
            {
                ChatId = SelectedDialog.ChatId
            }, cancellationToken: _cancellationTS.Token);
            try
            {
                await foreach (var message in reply.ResponseStream.ReadAllAsync())
                {
                    Messages.Add(new Domain.Models.Message
                    {
                        AuthorId = Guid.Parse(message.AuthorId),
                        Content = message.Content,
                        Time = message.Time.ToDateTime()
                    });
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            { }
        }
    }
}