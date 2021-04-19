using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Constants;
using Domain.Protos;
using Domain.Repositories;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace DataAccess.Repositories
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
                    if (id == userId) list.Add(chat);
                }
            }

            return list;
        }

        public async Task<Chat> GetChatAsync(string chatId)
        {
            var chat = await Chats
                .Find(Builders<ChatDocument>.Filter.Eq(c => c.Id, chatId))
                .FirstOrDefaultAsync();
            
            return _mapper.Map<Chat>(chat);
        }
        
        public async Task AddMessageAsync(string chatId, Message message)
        {
            var filter = Builders<ChatDocument>.Filter.Eq(c => c.Id, chatId);
                
            var update = Builders<ChatDocument>.Update.Push(c => c.History, _mapper.Map<Domain.Models.Message>(message));
            await Chats.UpdateOneAsync(filter, update);
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await Chats.InsertOneAsync(_mapper.Map<ChatDocument>(chat));
        }
    }
}