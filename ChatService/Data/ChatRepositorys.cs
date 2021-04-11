using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.DbModels;
using Domain.Repositories;
using Messenger.ChatService.Protos;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatService.Data
{
    public class ChatRepositorys : IChatRepository
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;
        
        private IMongoCollection<ChatDocument> Chats => _repository.GetCollection<ChatDocument>(CollectionsNames.Chats);
        
        public ChatRepositorys(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChatDocument>> GetChatsAsync(string userId)
        {
            var chats = await Chats
                .Find(new BsonDocument()).ToListAsync();
            
            return (from chat in chats from id in chat.UserIds where id == userId select chat).ToList();
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