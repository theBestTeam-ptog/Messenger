using System.Windows;
using System.Windows.Controls;
using Domain.Mappers;
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
            Application.Current.MainWindow.Height = MinHeight;
            Application.Current.MainWindow.Width = MinWidth;
            Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(MainWindow.Pages.Registration);
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            App.InitApp();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            var reply = client.TakeUser(new PickUpUser {Login = login.Text});
            var p = reply.User;
            var map = new UserDocumentMapper();
        }
    }
}