using System;
using System.Windows;
using Api.Helpers;
using JetBrains.Annotations;

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
            
            frame.NavigationService.Navigate(new Uri("Pages/PageNoDialog.xaml", UriKind.Relative));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            frame.Source = null;
            frame.Navigate(new Uri("Pages/Dialog.xaml", UriKind.Relative));
        }
    }
}