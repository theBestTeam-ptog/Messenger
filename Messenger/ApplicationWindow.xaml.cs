﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Domain.Mappers;
using Domain.Models;
using Grpc.Core;
using Grpc.Net.Client;
using Domain.Protos;
using Messenger.Utils;
using Messenger.ViewModels;
using Chat = Domain.Protos.Chat;
using Message = Domain.Models.Message;

namespace Messenger
{
    public partial class ApplicationWindow
    {
        private readonly IDialogListViewModel _dialogList;
        private readonly ISearchResultViewModel _searchResult;
        private readonly IMapper<Chat, ChatViewModel> _chatViewModelMapper;
        
        public ApplicationWindow( 
            IDialogListViewModel dialogList, 
            ISearchResultViewModel searchResult, 
            IMapper<Chat, ChatViewModel> chatViewModelMapper)
        {
            var chats1 = new ObservableCollection<ChatViewModel>();
            _dialogList = dialogList;
            _searchResult = searchResult;
            _chatViewModelMapper = chatViewModelMapper;

            InitializeComponent();
            
            // var chats2 = new List<ChatViewModel>
            // {
            //     new ChatViewModel {ChatName = "chat1"},
            //     new ChatViewModel {ChatName = "chat2"},
            //     new ChatViewModel {ChatName = "chat3"},
            // };
            //
            // _dialogList.Chats = chats2;//App.CurrentUser.Chats;
            //
            // list.ItemTemplate = (DataTemplate) list.FindResource("itemTemplate");
            // list.DataContext = _dialogList.Chats;

            dialogFrame.Navigate(new Uri(Constants.PagesUris.EmptyDialog, UriKind.Relative));
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Searcher.Text.Trim()))
            {
                list.ItemTemplate = (DataTemplate) list.FindResource("itemTemplate");
                list.DataContext = _dialogList.Chats;
                return;
            }
            
            
            var users = new List<UserViewModel>
            {
                new UserViewModel{UserName = "user1"},
                new UserViewModel{UserName = "user2"},
                new UserViewModel{UserName = "user3"},
            };
            
            list.ItemTemplate = (DataTemplate) list.FindResource("searchItemTemplate");
            _searchResult.Users?.Clear();
            _searchResult.Users = users;
            list.DataContext = _searchResult.Users;
        }

        private async void OpenDialog(object sender, MouseButtonEventArgs e)
        {
            var chatId = ((sender as ListBoxItem).DataContext as ChatViewModel).ChatId;
            var client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
            var reply = client.JoinChat(new GetChat
            {
                ChatId = chatId
            });
            var messages = new ObservableCollection<Message>();
            var token = new CancellationTokenSource().Token;
            while(await reply.ResponseStream.MoveNext(token) && token.IsCancellationRequested)
                await foreach(var message in reply.ResponseStream.ReadAllAsync(token))
                {
                    messages.Add(new Message
                    {
                        AuthorId = Guid.Parse((ReadOnlySpan<char>) message.AuthorId),
                        Content = message.Content,
                        Time = message.Time.ToDateTime()
                    });
                }
            
            dialogFrame.Navigate(new Uri(Constants.PagesUris.Dialog, UriKind.Relative));
        }

        /// <summary>
        /// Открывает меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Закрывает меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMenu_Click(object sender, RoutedEventArgs e)
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
            }
        }
    }
}