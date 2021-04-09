using System.Windows;
using Core;
using Core.Log;
using Domain.Models;
using Messenger.Pages;
using Messenger.ViewModels;
using Ninject;

namespace Messenger
{
    public partial class App : Application
    {
        public static UserViewModel CurrentUser; 
        private static readonly ILogger<App> Logger = new ConsoleLogger<App>();
        private static IKernel _container;

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
            _container.Bind<ApplicationWindow>().ToSelf().InSingletonScope();
            _container.Bind<IDialogListViewModel>().To<DialogListViewModel>().InSingletonScope();
            _container.Bind<ISearchResultViewModel>().To<SearchResultViewModel>().InSingletonScope();

            Logger.Info($"{nameof(Messenger.MainWindow)} put into container");
            Logger.Info($"{nameof(ApplicationWindow)} put into container");
            //_container.Bind<>()
        }

        public static void InitApp()
        {
            var app = _container.Get<ApplicationWindow>();
            app.Show();
            Current.MainWindow.Close();
        }
    }
}