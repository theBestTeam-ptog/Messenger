using System;
using System.Windows;

namespace Messenger
{
    public partial class ApplicationWindow : Window
    {
        public ApplicationWindow()
        {
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