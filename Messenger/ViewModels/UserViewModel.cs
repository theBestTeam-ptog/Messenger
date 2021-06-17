using System;
using System.Collections.Generic;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace Messenger.ViewModels
{
    [UsedImplicitly]
    public sealed class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Image ProfileImage { get; set; }
        public List<ChatViewModel> Chats { get; set; }
        public bool InNetwork { get; set; }
    }
}