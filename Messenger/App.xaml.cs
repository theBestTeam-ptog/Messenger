using System.Windows;

namespace Messenger
{
    public partial class App
    {
        //private static IKernel _container;

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
        }
    }
}