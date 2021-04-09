using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.DbModels;
using Domain.Repositories;
using Messenger.ChatService.Protos;
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

        public async Task<List<ChatDocument>> GetChats(IEnumerable<string> ids)
        {
            return await Chats
                .Find(Builders<ChatDocument>.Filter.All(c => c.UserIds, ids))
                .ToListAsync();
        }

        public async Task<Chat> GetChat(string id)
        {
            var chat = await Chats.Find(Builders<ChatDocument>.Filter.Eq(c => c.Id, id)).FirstOrDefaultAsync();
            return _mapper.Map<Chat>(chat);
        }

        public async Task AddMessage(string chatId, Message message)
        {
            var chat = await GetChat(chatId);
            chat?.History.Add(message);
        }

        public Task Create(Chat chat)
        {
            throw new System.NotImplementedException();
        }
    }
}