using System;
using System.Windows;
using Api.Helpers;
using JetBrains.Annotations;
using Messenger.Pages;

namespace Messenger
{
    [UsedImplicitly]
    public partial class MainWindow : Window
    {
        private readonly IUserHelper _userHelper;
        public MainWindow(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            InitializeComponent();
            OpenPage(Pages.Authorization);
        }
        public enum Pages
        {
            Authorization,
            Registration
        }
        public void OpenPage(Pages pages)
        {
            if (pages == Pages.Authorization)
                Frame.Navigate((new Authorization(this)));
            else 
                Frame.Navigate(new Registration(this));
        }
        private void test(object sender, RoutedEventArgs e)
        {
            App.InitApp();
            Close();
        }
    }
}