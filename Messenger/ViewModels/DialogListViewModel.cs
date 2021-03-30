using System.Collections.Generic;
using Domain.Models;
using JetBrains.Annotations;

namespace Messenger.ViewModels
{
    [UsedImplicitly]
    public sealed class DialogListViewModel : IDialogListViewModel
    {
        public List<UserViewModel> Users { get; set; } 
    }
}