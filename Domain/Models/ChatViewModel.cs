using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Models
{
    public sealed class ChatViewModel
    {
        public string ChatName { get; set; }
        public ObservableCollection<Message> History { get; set; }
        public string ChatId { get; set; }
    }
}