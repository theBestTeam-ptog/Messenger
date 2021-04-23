using System;
using System.Linq;
using System.Windows.Controls;
using Core.IoC;
using DataAccess.Mappers;
using Domain.Models;
using Domain.Protos;
using Messenger.Service;
using Messenger.ViewModels.Base;

namespace Messenger.ViewModels
{
    public class AuthorizationControlPage : ViewModelBase
    {
        private static RelayCommand _authorization;
        private string _login;
        private static string passwordInfo;
        private RelayCommand _password;
        public string LoginInfo {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged(nameof(LoginInfo));
            }
        }

        public static RelayCommand Authorization => _authorization ??= new RelayCommand(_ =>
        {
            // var service = Bootstrapper.Container.GetInstance(typeof(ChatClient)) as ChatClient;
            var service = new ChatClient();
            var reply = service.Client.Authorization(new GetUser()
            {
                // Login = LoginInfo,
                Password = passwordInfo
            });
            var chatReply = service.Client.TakeChats(new GetChats() {UserId = reply.User.Id});
            var mapper = Bootstrapper.Container.GetInstance(typeof(ChatViewModelMapper)) as ChatViewModelMapper;

            UserInfoViewModel.UserCurrent = new UserViewModel()
            {
                Chats = chatReply.Chats.Select(x => mapper.Map(x)).ToList(),
                Id = Guid.Parse(reply.User.Id),
                UserName = reply.User.UserName,
                InNetwork = reply.User.InNetwork,
                ProfileImage = string.Empty
            };
        });

        // public RelayCommand Login
        // {
        //     get
        //     {
        //         return _login ??= new RelayCommand(obj =>
        //         {
        //             var login = obj as string;
        //
        //             loginInfo = login;
        //         });
        //     }
        // }

        public RelayCommand Password
        {
            get
            {
                return _password ??= new RelayCommand(obj =>
                {
                    var password = obj as PasswordBox;

                    passwordInfo = password.Password;
                });
            }
        }
    }
}