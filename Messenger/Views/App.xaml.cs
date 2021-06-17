using System.Windows;
using Domain.Models;
using Messenger.ViewModels;

namespace Messenger.Views
{
    public partial class App
    {
        public static UserViewModel CurrentUser;
        
        public App()
        {
            Bootstrapper.Init();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = Bootstrapper.Container.GetInstance<MainWindow>();
            mainWindow.Show();
        }

        public static void InitApp()
        {
            var applicationWindow = Bootstrapper.Container.GetInstance<ApplicationWindow>();
            applicationWindow.Show();
            Current.MainWindow?.Close();
        }
    }
}