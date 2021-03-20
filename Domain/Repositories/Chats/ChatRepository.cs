using System.Threading.Tasks;
using Domain.Constants;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace Domain.Repositories.Chats
{
    [UsedImplicitly]
    public sealed class ChatRepository : IChatRepository
    {
        private readonly Repository _repository;
        private readonly IMapper<Chat, ChatDocument> _chatDocumentMapper;
        private readonly IMapper<ChatDocument, Chat> _documentChatMapper;

        public ChatRepository(Repository repository, 
            ChatDocumentMapper chatDocumentMapper, 
            IMapper<ChatDocument, Chat> documentChatMapper)
        {
            _repository = repository;
            _chatDocumentMapper = chatDocumentMapper;
            _documentChatMapper = documentChatMapper;
        }

        private IMongoCollection<ChatDocument> Chats => _repository.Database.GetCollection<ChatDocument>(CollectionsNames.Chats);
        
        public async Task<Chat> Get(string id)
        {
            var chat = await Chats.Find(Builders<ChatDocument>.Filter.Eq(c => c.Id, id)).FirstOrDefaultAsync();
            return _documentChatMapper.Map(chat);
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