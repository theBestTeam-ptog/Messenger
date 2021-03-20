using System;

namespace Domain.Models
{
    public sealed class Message
    {
        // содержимое сообщения
        public string Content { get; set; }
        // Id отправителя сообщения
        public string AuthorId { get; set; }
        // время отправки сообщения
        public DateTime Time { get; set; }
    }
}