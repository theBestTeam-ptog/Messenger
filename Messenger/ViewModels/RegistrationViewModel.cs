using System.Windows.Controls;
using Domain.Protos;
using Messenger.Service;
using Messenger.ViewModels.Base;

namespace Messenger.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private RelayCommand _registrationCommand;
        private string _name;
        private string _login;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        public RelayCommand RegistrationCommand => _registrationCommand ??= new RelayCommand(async obj =>
        {
            var client = Bootstrapper.Container.GetInstance<ChatClient>();
            var password = obj as PasswordBox;

            await client.Client.RegistrationUserAsync(new UserCreate
            {
                Login = Login,
                Password = password.Password,
                UserName = Name
            });
        });
    }
}