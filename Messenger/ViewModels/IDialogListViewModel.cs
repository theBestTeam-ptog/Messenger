using System.Collections.Generic;
using Domain.Models;

namespace Messenger.ViewModels
{
    public interface IDialogListViewModel
    {
        List<ChatViewModel> Chats { get; set; }
    }
}