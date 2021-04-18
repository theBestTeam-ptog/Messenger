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
            Application.Current.MainWindow.Height = 330;
            Application.Current.MainWindow.Width = 415;
            //Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            // var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            //
            // var chatReply = client.CreateUser(new UserCreate
            // {
            //     User = new User
            //     {
            //         Authorize = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
            //         InNetwork = true,
            //         HasImage = false,
            //         Id = Guid.NewGuid().ToString(),
            //         Login = LoginBox.Text,
            //         Password = PasswordBox.Password,
            //         Private = false,
            //         Registration = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
            //         UserName = UserNameBox.Text
            //     }
            // });

            _mainWindow.OpenPage(Utils.Pages.Authorization);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(Utils.Pages.Authorization);
        }
    }
}