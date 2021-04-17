using System.Windows;
using System.Windows.Controls;
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
            //var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            //var reply =  await client.TakeUserAsync(new PickUpUser {Login = login.Text, Password = password.Password});
            
            //if (reply.Response != Response.Ok) throw new NullReferenceException();
            //var chatReply = await client.TakeChatsAsync(new TakeChatRequest() {UserId = reply.User.Id});
            //var user = reply.User;
            App.InitApp();
            // var chat = new Domain.Models.Chat()
            // {
            //     History = chatReply.Chats.ToList()
            // };
            // App.CurrentUser = new UserViewModel
            // {
            //     ChatsIds = chatReply.Chats.ToList(),
            //     Id = Guid.Parse(user.Id),
            //     ProfileImage = null,
            //     InNetwork = user.InNetwork,
            //     UserName = user.UserName
            // };
        }
    }
}