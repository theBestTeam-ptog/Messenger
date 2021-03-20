using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Repositories.Users
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository() : base() { }
        private readonly UserDocumentMapper _userDocumentMapper = new UserDocumentMapper();

        private IMongoCollection<UserDocument> Users => Database.GetCollection<UserDocument>(CollectionsNames.Users);
        
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
            await Users.InsertOneAsync(_userDocumentMapper.Map(user));
        }
        
        public IEnumerable<User> GetUsersByName(string name)
        {
            return Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.UserName, name))
                .ToEnumerable()
                .Select(_userDocumentMapper.Map);
        }
    }
}