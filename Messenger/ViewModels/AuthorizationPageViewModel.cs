using System;
using System.Linq;
using System.Windows.Controls;
using DataAccess.Mappers;
using Domain.Models;
using Domain.Protos;
using Messenger.Service;
using Messenger.ViewModels.Base;
using Messenger.Views;

namespace Messenger.ViewModels
{
    public class AuthorizationControlPage : ViewModelBase
    {
        private RelayCommand _authorizationCommand;
        private string _login;
        
        public string LoginInfo {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged(nameof(LoginInfo));
            }
        }

        public RelayCommand AuthorizationCommand => _authorizationCommand ??= new RelayCommand(obj =>
        {
            var client = Bootstrapper.Container.GetInstance<ChatClient>();
            var mapper = Bootstrapper.Container.GetInstance<ChatViewModelMapper>();
            
            var password = obj as PasswordBox;
            var reply = client.Client.Authorization(new GetUser
            {
                Login = LoginInfo,
                Password = password.Password
            });
            
            var chatReply = client.Client.TakeChats(new GetChats
            {
                UserId = reply.User.Id
            });
            
            var user = new UserViewModel
            {
                Chats = chatReply.Chats.Select(x => mapper.Map(x)).ToList(),
                Id = Guid.Parse(reply.User.Id),
                UserName = reply.User.UserName,
                InNetwork = reply.User.InNetwork,
                ProfileImage = string.Empty
            };

            MainPageViewModel.CurrentUser = user;
            App.InitApp();
        });
    }
}