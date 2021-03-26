using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Domain.Models
{
    [UsedImplicitly]
    public sealed class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public List<string> Chats { get; set; }
        public bool InNetwork { get; set; }
    }
}