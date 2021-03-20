using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Repositories.Users
{
    [UsedImplicitly]
    public sealed class UserRepository : IUserRepository
    {
        private readonly Repository _repository;
        private readonly IMapper<User, UserDocument> _userDocumentMapper;
        private readonly IMapper<UserDocument, User> _documentUserMapper;

        public UserRepository(Repository repository, 
            UserDocumentMapper userDocumentMapper, 
            IMapper<UserDocument, User> documentUserMapper)
        {
            _repository = repository;
            _userDocumentMapper = userDocumentMapper;
            _documentUserMapper = documentUserMapper;
        }

        private IMongoCollection<UserDocument> Users => _repository.Database.GetCollection<UserDocument>(CollectionsNames.Users);
        
        public async Task<User> GetUser(string id)
        {
            var user = await Users.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            return _documentUserMapper.Map(user);
        }

        // проверка есть ли уже такой логин в бд 
        public async Task<bool> CheckLogin(string login) =>
            await Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.Login, login))
                .FirstOrDefaultAsync() is null;
        
        public async Task<User> GetUserValidation(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;
            
            var user = await Users.Find(Builders<UserDocument>.Filter.Eq(u=> u.Login, login)).FirstOrDefaultAsync();
            return _documentUserMapper.Map(user);
        }

        public async Task Create(User user)
        {
            await Users.InsertOneAsync(_userDocumentMapper.Map(user));
        }
        
        public IEnumerable<User> GetUsersByName(string name)
        {
            return Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.UserName, name))
                .ToEnumerable()
                .Select(_documentUserMapper.Map);
        }
    }
}