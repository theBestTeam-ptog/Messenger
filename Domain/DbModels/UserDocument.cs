using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DbModels
{
    public sealed class UserDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [BsonIgnoreIfNull]
        public List<string> Chats { get; set; }
        [BsonIgnoreIfNull]
        public string ProfileImage { get; set; }
        [BsonIgnoreIfDefault]
        public bool Private { get; set; }
        public DateTime Registration { get; set; }
    }
}