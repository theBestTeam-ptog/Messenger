using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Api.Helpers;
using Domain.Models;
using Messenger.ViewModels;

namespace Messenger
{
    public partial class ApplicationWindow : Window
    {
        public static readonly UserViewModel CurrentUser;

        private readonly IChatHelper _chatHelper;
        private readonly IUserHelper _userHelper;
        private readonly IDialogListViewModel _dialogList;
        private readonly ISearchResultViewModel _searchResult;


        public ApplicationWindow(IChatHelper chatHelper,
            IUserHelper userHelper,
            IDialogListViewModel dialogList,
            ISearchResultViewModel searchResult)
        {
            _chatHelper = chatHelper;
            _userHelper = userHelper;
            _dialogList = dialogList;
            _searchResult = searchResult;

            InitializeComponent();

            //list.DataContext = _dialogList;

            dialogFrame.Navigate(new Uri("Pages/PageNoDialog.xaml", UriKind.Relative));

            _dialogList.Users = new List<UserViewModel>
            {
                new UserViewModel {UserName = "user1"},
                new UserViewModel {UserName = "user2"},
                new UserViewModel {UserName = "user3"},
            };
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Searcher.Text?.Trim()))
            {
                list.DataContext = _dialogList;
                return;
            }

            var results = _userHelper.Search(Searcher.Text).ToList();
            if (results.Count == 0) return;

            _searchResult.Users?.Clear();
            _searchResult.Users = results.ToList();
            list.DataContext = _searchResult;
        }

        private void OpenDialog(object sender, MouseButtonEventArgs e)
        {
            dialogFrame.Navigate(new Uri("Pages/Dialog.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Открывает меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Закрывает меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Реализует переключение между элементами управления приложением.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMenu.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                // В зависимости от выбранного элемента происходит переход на выбранную элемент управления.
                case "Friends":
                    //usc - адрес того элемента управления, на который ссылаемся.
                    usc = new UserControl();
                    GridMenu.Children.Add(usc);
                    break;
                case "Groups":
                    usc = new UserControl();
                    GridMenu.Children.Add(usc);
                    break;
                case "Settings":
                    usc = new UserControl();
                    GridMenu.Children.Add(usc);
                    break;
                case "Help":
                    usc = new UserControl();
                    GridMenu.Children.Add(usc);
                    break;
                case "ChangeTheme":
                    usc = new UserControl();
                    GridMenu.Children.Add(usc);
                    break;
            }
        }
    }
}