using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Domain.Models;
using Messenger.ViewModels.Base;

namespace Messenger.ViewModels
{
    public class DialogViewModel : ViewModelBase
    {
        public Image DialogImage { get; set; }
        public string DialogName { get; set; }
        public Guid _dialogId;
        public List<Message> Messages { get; set; }
    }
}