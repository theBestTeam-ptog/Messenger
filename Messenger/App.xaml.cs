using System.Windows;
using Core;
using Core.Log;
using Ninject;

namespace Messenger
{
    public partial class App : Application
    {
        private static readonly ILogger<App> Logger = new ConsoleLogger<App>();
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            var window = _container.Get<MainWindow>();
            window.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel(new Registry());

            _container.Bind<MainWindow>().ToSelf().InSingletonScope();
            
            Logger.Info($"{nameof(Messenger.MainWindow)} put into container");
            //_container.Bind<>()
        }
    }
}