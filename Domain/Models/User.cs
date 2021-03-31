using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<string> Chats { get; set; }
        public bool Private { get; set; }
        public DateTime Registration { get; set; }
        public DateTime Authorize { get; set; }
        // пусть пока будет стринг
        public string ProfileImage { get; set; }
        public bool InNetwork { get; set; }
        
        // public bool HasImage() => !string.IsNullOrWhiteSpace(ProfileImage);
    }
}