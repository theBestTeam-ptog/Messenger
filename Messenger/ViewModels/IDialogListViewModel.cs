using System.Collections.Generic;
using Domain.Models;

namespace Messenger.ViewModels
{
    public interface IDialogListViewModel
    {
        List<UserViewModel> Users { get; set; }
    }
}