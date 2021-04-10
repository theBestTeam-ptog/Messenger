using System.Threading.Tasks;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Constants;
using Domain.Mappers;
using Domain.Models;
using Domain.Repositories;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace DataAccess.Repositories.Chats
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatRepository : IChatRepository
    {
        private readonly Repository _repository;
        private readonly IDuplexMapper<Chat, ChatDocument> _chatDocumentMapper;

        public ChatRepository(Repository repository, 
            IDuplexMapper<Chat, ChatDocument> chatDocumentMapper) 
        {
            _repository = repository;
            _chatDocumentMapper = chatDocumentMapper;
        }

        private IMongoCollection<ChatDocument> Chats => _repository.GetCollection<ChatDocument>(CollectionsNames.Chats);
        
        public async Task<Chat> Get(string id)
        {
            var chat = await Chats.Find(Builders<ChatDocument>.Filter.Eq(c => c.Id, id)).FirstOrDefaultAsync();
            return _chatDocumentMapper.Map(chat);
        }

        public async Task AddMessage(string chatId, Message message)
        {
            var chat = await Get(chatId);
            chat?.History.Add(message);
        }
        
        public async Task Create(Chat chat)
        {
            await Chats.InsertOneAsync(_chatDocumentMapper.Map(chat));
        }
    }
}