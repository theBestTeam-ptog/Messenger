using System;
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
            // Bootstrapper.Container.GetInstance<Authorization>()
            // Bootstrapper.Container.GetInstance<Registration>()
            if (pages == Utils.Pages.Authorization)
                Frame.Navigate(new Authorization(this));
            else
                Frame.Navigate(new Registration(this));
        }
    }
}