﻿using System.Collections.Generic;
using Core.IoC;
using Domain.Models;
using JetBrains.Annotations;

namespace Messenger.ViewModels
{
    [PutInIoC, UsedImplicitly]
    public sealed class DialogListViewModel : IDialogListViewModel
    {
        public List<UserViewModel> Users { get; set; } 
    }
}