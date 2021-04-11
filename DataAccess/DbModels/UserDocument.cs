using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.DbModels
{
    public sealed class UserDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [BsonIgnoreIfDefault]
        public List<string> ChatsIds { get; set; }
        [BsonIgnoreIfDefault]
        public string ProfileImage { get; set; }
        [BsonIgnoreIfDefault]
        public bool Private { get; set; }
        [BsonIgnoreIfDefault]
        public DateTime Registration { get; set; }
        [BsonIgnoreIfDefault]
        public DateTime Authorize { get; set; }
        [BsonIgnoreIfDefault]
        public bool InNetwork { get; set; } // зашел или вышел из аккаунта
    }
}