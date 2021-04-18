using System.Windows;
using JetBrains.Annotations;
using Messenger.Pages;

namespace Messenger
{
    [UsedImplicitly]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(Utils.Pages.Authorization);
        }

        public void OpenPage(Utils.Pages pages)
        {
            if (pages == Utils.Pages.Authorization)
                Frame.Navigate(Bootstrapper.Container.GetInstance<Authorization>());
            else
                Frame.Navigate(Bootstrapper.Container.GetInstance<Registration>());
        }
    }
}