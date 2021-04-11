﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Constants;
using Domain.Repositories;
using JetBrains.Annotations;
using MongoDB.Driver;
using Messenger.ChatService.Protos;

namespace DataAccess.Repositories.Chats
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatRepository : IChatRepository
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;

        private IMongoCollection<ChatDocument> Chats => _repository.GetCollection<ChatDocument>(CollectionsNames.Chats);

        public ChatRepository(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Chat>> GetChatsAsync(string userId)
        {
            var chats = _mapper.Map<List<Chat>>(await Chats.Find(Builders<ChatDocument>.Filter.Empty).ToListAsync());
            var list = new List<Chat>();
            
            foreach (var chat in chats)
            {
                foreach (var id in chat.UserIds)
                {
                    if(id == userId) list.Add(chat);
                }
            }
            
            return list;
        }

        public async Task<Chat> GetChatAsync(string chatId)
        {
            var chat = await Chats.Find(Builders<ChatDocument>.Filter.Eq(c => c.Id, chatId)).FirstOrDefaultAsync();
            return _mapper.Map<Chat>(chat);
        }

        public async Task AddMessageAsync(string chatId, Message message)
        {
            var chat = await GetChatAsync(chatId);
            chat?.History.Add(message);
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await Chats.InsertOneAsync(_mapper.Map<ChatDocument>(chat));
        }
    }
}