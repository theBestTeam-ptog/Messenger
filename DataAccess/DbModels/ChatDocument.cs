using System.Collections.Generic;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.DbModels
{
    public sealed class ChatDocument
    {
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public List<string> UserIds {get;set;}
        public List<Message> History { get; set; }
        
        public UserInfoDocument[] UserInfos { get; set; }
    }

    public sealed class UserInfoDocument
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    } 
}