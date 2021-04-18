using System;
using System.Windows;
using System.Windows.Controls;
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
            Application.Current.MainWindow.Height = 400;
            Application.Current.MainWindow.Width = 415;
        }
        
        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(Utils.Pages.Registration);
        }
        
        private async void LogButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            var reply =  await client.TakeUserAsync(new PickUpUser {Login = LoginBox.Text, Password = PasswordBox.Password});
            var userReply = reply.User; 
            
            // todo убрать это в App.OnStartUp()
            // var chatReply = await client.TakeChatsAsync(new TakeChatRequest {UserId = reply.User.Id});
            // var chatViewModels = chatReply.Chats.Select(x => _mapper.Map(x));

            var user = new UserViewModel
            {
                Id = Guid.Parse(userReply.Id),
                InNetwork = userReply.InNetwork,
                UserName = userReply.UserName
            };

            App.CurrentUser = user; 
            
            App.InitApp();
        }
    }
}