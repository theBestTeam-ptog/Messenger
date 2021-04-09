using System.Windows;
using System.Windows.Controls;

namespace Messenger.Pages
{
    public partial class Registration : Page
    {
        private readonly MainWindow _mainWindow;
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Application.Current.MainWindow.Height = MinHeight;
            Application.Current.MainWindow.Width = MinWidth;
            Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(MainWindow.Pages.Authorization);
        }
    }
}