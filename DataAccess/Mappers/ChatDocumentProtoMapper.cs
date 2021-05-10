using System;
using System.Linq;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Mappers;
using Domain.Protos;
using Google.Protobuf.WellKnownTypes;
using JetBrains.Annotations;

namespace DataAccess.Mappers
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatDocumentProtoMapper : IDuplexMapper<Chat, ChatDocument>
    {
        public Chat Map(ChatDocument source) => source == null
            ? null
            : new Chat
            {
                Id = source.Id,
                UserIds = {source.UserIds},
                History = {source.History.Select(Map)},
                UserInfos = {source.UserInfos.Select(Map)}
            };

        public ChatDocument Map(Chat source) => source == null
            ? null
            : new ChatDocument
            {
                Id = source.Id,
                UserInfos = source.UserInfos.Select(Map).ToArray(),
                History = source.History.Select(Map).ToList(),
                UserIds = source.UserIds.ToList(),
            };

        private Message Map(Domain.Models.Message s) => new Message
        {
            Content = s.Content,
            AuthorId = s.AuthorId.ToString(),
            AuthorName = s.AuthorName,
            Time = Timestamp.FromDateTime(s.Time),
        };

        private Domain.Models.Message Map(Message s) => new Domain.Models.Message
        {
            Content = s.Content,
            AuthorId = Guid.Parse(s.AuthorId),
            AuthorName = s.AuthorName,
            Time = s.Time.ToDateTime(),
        };
        
        private UserInfo Map(UserInfoDocument s) => new UserInfo
        {
            Id = s.Id,
            UserName = s.UserName,
        };
        
        private UserInfoDocument Map(UserInfo s) => new UserInfoDocument
        {
            Id = s.Id,
            UserName = s.UserName,
        };
    }
}