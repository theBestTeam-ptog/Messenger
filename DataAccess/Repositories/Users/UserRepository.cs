using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Constants;
using Domain.Mappers;
using Domain.Models;
using Domain.Repositories;
using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories.Users
{
    [PutInIoC, UsedImplicitly]
    public sealed class UserRepository : IUserRepository
    {
        private readonly Repository _repository;
        private readonly IDuplexMapper<User, UserDocument> _userDocumentMapper;
        private readonly IMapper<UserDocument, UserViewModel> _userViewModelMapper;

        public UserRepository(Repository repository, 
            IDuplexMapper<User, UserDocument> userDocumentMapper, 
            IMapper<UserDocument, UserViewModel> userViewModelMapper)
        {
            _repository = repository;
            _userDocumentMapper = userDocumentMapper;
            _userViewModelMapper = userViewModelMapper;
        }

        private IMongoCollection<UserDocument> Users => _repository.GetCollection<UserDocument>(CollectionsNames.Users);
        
        public async Task<User> GetUser(string id)
        {
            var user = await Users.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            return _userDocumentMapper.Map(user);
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
            return _userDocumentMapper.Map(user);
        }

        public async Task Create(User user)
        {
            var doc = _userDocumentMapper.Map(user);
            await Users.InsertOneAsync(doc);
        }
        
        public IEnumerable<User> GetUsersByName(string name)
        {
            return Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.UserName, name))
                .ToEnumerable()
                .Select(_userDocumentMapper.Map);
        }

        [ItemCanBeNull]
        public IEnumerable<UserViewModel> Search(string suggest)
        {
            return Users.Find(
                    Builders<UserDocument>.Filter.Where(u => u.UserName.StartsWith(suggest))
                )
                .ToEnumerable()
                .Select(_userViewModelMapper.Map);
        }
    }
}