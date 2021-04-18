using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Mappers;
using Domain.Mappers;
using Domain.Models;
using Grpc.Net.Client;
using Messenger.ChatService.Protos;
using Chat = Messenger.ChatService.Protos.Chat;

namespace Messenger.Pages
{
    public partial class Authorization : Page
    {
        private readonly Lazy<MainWindow> _mainWindow;
        private readonly IMapper<Chat, ChatViewModel> _mapper; 
        
        public Authorization(Lazy<MainWindow> mainWindow,
            IMapper<Chat, ChatViewModel> mapper)
        {
            InitializeComponent();
            _mapper = mapper;
            _mainWindow = mainWindow;
            Application.Current.MainWindow.Height = 330;
            Application.Current.MainWindow.Width = 415;
            //Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
        }
        
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Value.OpenPage(Utils.Pages.Registration);
        }
        
        private async void LogButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            var reply =  await client.TakeUserAsync(new PickUpUser {Login = LoginBox.Text, Password = PasswordBox.Password});
            var userReply = reply.User; 
            
            var chatReply = await client.TakeChatsAsync(new TakeChatRequest {UserId = reply.User.Id});
            
            var chatViewModels = chatReply.Chats.Select(x => _mapper.Map(x));

            var user = new UserViewModel
            {
                Chats = chatViewModels.ToList(),
                Id = Guid.Parse(userReply.Id),
                InNetwork = userReply.InNetwork,
                UserName = userReply.UserName
            };

            App.CurrentUser = user;
            App.InitApp();
        }
    }
}