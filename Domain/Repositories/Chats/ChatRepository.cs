using System.Threading.Tasks;
using Domain.Constants;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using MongoDB.Driver;

namespace Domain.Repositories.Chats
{
    public class ChatRepository : Repository, IChatRepository
    {
        public ChatRepository() : base() { }
        private readonly ChatDocumentMapper _chatDocumentMapper = new ChatDocumentMapper();
        private IMongoCollection<ChatDocument> Chats => Database.GetCollection<ChatDocument>(CollectionsNames.Chats);
        
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