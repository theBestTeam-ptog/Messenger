using System;
using System.Windows;
using System.Windows.Controls;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Messenger.ChatService.Protos;

namespace Messenger.Pages
{
    public partial class Registration : Page
    {
        private readonly MainWindow _mainWindow;
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Application.Current.MainWindow.Height = MinHeight;
            Application.Current.MainWindow.Width = MinWidth;
            Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));

            var chatReply = client.CreateUser(new UserCreate
            {
                User = new User
                {
                    Authorize = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                    InNetwork = true,
                    HasImage = false,
                    Id = Guid.NewGuid().ToString(),
                    Login = login.Text,
                    Password = password.Password,
                    Private = false,
                    Registration = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                    UserName = userName.Text
                }
            });

            _mainWindow.OpenPage(MainWindow.Pages.Authorization);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(MainWindow.Pages.Authorization);
        }
    }
}