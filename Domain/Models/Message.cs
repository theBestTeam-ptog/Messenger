using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public sealed class Message
    {
        // содержимое сообщения
        public string Content { get; set; }
        // Id отправителя сообщения
        [BsonRepresentation(BsonType.String)]
        public Guid AuthorId { get; set; }
        // имя отправителя
        public string AuthorName { get; set; }
        // время отправки сообщения
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Time { get; set; }
    }
}