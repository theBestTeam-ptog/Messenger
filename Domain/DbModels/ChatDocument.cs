using System.Collections.Generic;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DbModels
{
    public sealed class ChatDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<string> UserIds {get;set;}
        public List<Message> History { get; set; }
    }
}