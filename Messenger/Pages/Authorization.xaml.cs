using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Mappers;
using Domain.Models;
using Grpc.Net.Client;
using Messenger.ChatService.Protos;

namespace Messenger.Pages
{
    public partial class Authorization : Page
    {
        private readonly MainWindow _mainWindow;
        public Authorization(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Application.Current.MainWindow.Height = 330;
            Application.Current.MainWindow.Width = 415;
            //Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(MainWindow.Pages.Registration);
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            var reply =  await client.TakeUserAsync(new PickUpUser {Login = login.Text, Password = password.Password});
            var userReply = reply.User; 
            
            var chatReply = await client.TakeChatsAsync(new TakeChatRequest {UserId = reply.User.Id});
            
            var mapper = Bootstrapper.Container.GetInstance<ChatViewModelMapper>();
            var chatViewModels = chatReply.Chats.Select(x => mapper.Map(x));

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