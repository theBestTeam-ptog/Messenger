using System.Windows;
using System.Windows.Controls;
using Domain.Mappers;
using Grpc.Net.Client;
using Messenger.ChatService.Protos;

namespace Messenger.Pages
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
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