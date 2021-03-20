using System;
using System.Windows;
using Api.Helpers;

namespace Messenger
{
    public partial class ApplicationWindow : Window
    {
        private readonly IChatHelper _chatHelper;
        private readonly IUserHelper _userHelper;
        
        public ApplicationWindow(IChatHelper chatHelper, 
            IUserHelper userHelper)
        {
            InitializeComponent();
            
            _chatHelper = chatHelper;
            _userHelper = userHelper;
            
            frame.NavigationService.Navigate(new Uri("Pages/PageNoDialog.xaml", UriKind.Relative));
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            frame.Source = null;
            frame.Navigate(new Uri("Pages/Dialog.xaml", UriKind.Relative));
        }
    }
}