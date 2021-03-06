using System;
using System.Windows;
using System.Windows.Controls;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Domain.Protos;
using JetBrains.Annotations;
using Messenger.Views;

namespace Messenger.Pages
{
    [UsedImplicitly]
    public partial class Registration : Page
    {
        private readonly MainWindow _mainWindow;
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Application.Current.MainWindow.Height = 400;
            Application.Current.MainWindow.Width = 415;
        }
        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {
            // var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            //
            // var chatReply = await client.RegistrationUserAsync(new UserCreate
            // {
            //     User = new User
            //     {
            //         Authorize = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
            //         InNetwork = true,
            //         HasImage = false,
            //         Id = Guid.NewGuid().ToString(),
            //         Login = login.Text,
            //         Password = password.Password,
            //         Private = false,
            //         Registration = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
            //         UserName = userName.Text
            //     }
            // });
            //
            // _mainWindow.OpenPage(Utils.Pages.Authorization);
        }
        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(Utils.Pages.Authorization);
        }
    }
}