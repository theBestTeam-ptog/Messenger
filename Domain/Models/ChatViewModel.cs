using System.Collections.Generic;

namespace Domain.Models
{
    public sealed class ChatViewModel
    {
        public string ChatName { get; set; }
        public List<Message> History { get; set; }
        public string ChatId { get; set; }
    }
}