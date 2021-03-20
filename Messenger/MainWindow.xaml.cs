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
        }

        
    }
}