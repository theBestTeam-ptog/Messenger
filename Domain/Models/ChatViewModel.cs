using System.Collections.ObjectModel;

namespace Domain.Models
{
    public sealed class ChatViewModel
    {
        public string ChatName { get; set; }
        public ObservableCollection<Message> History { get; set; }
        public string ChatId { get; set; }
        
        public UserInfo[] UserInfos { get; set; }
        
    }

    public sealed class UserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}