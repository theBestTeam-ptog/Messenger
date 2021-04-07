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
    public class UserRepositorys : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;
        
        private IMongoCollection<UserDocument> Users => _repository.GetCollection<UserDocument>(CollectionsNames.Users);

        public UserRepositorys(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<User> GetUser(string id)
        {
            var user = await Users.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            return _mapper.Map<User>(user);
        }

        public async Task<bool> CheckLogin(string login)
        {
            return await Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.Login, login))
                .FirstOrDefaultAsync() is null;
        }

        public async Task<User> GetUserValidation(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) return null;
            
            var user = await Users
                .Find(Builders<UserDocument>.Filter.Eq(u=> u.Login, login))
                .FirstOrDefaultAsync();
            
            return _mapper.Map<User>(user);
        }

        public async Task Create(UserDocument user)
        {
            await Users.InsertOneAsync(user);
        }

        public IEnumerable<User> GetUsersByName(string name)
        {
            return Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.UserName, name))
                .ToEnumerable()
                .Select(_mapper.Map<User>);
        }

        public IEnumerable<User> Search(string suggest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> UserIsValid(string login, string password)
        {
            var user = await GetUserValidation(login);
            return user != null && user.Password.Equals(password) ? user : null;
        }
    }
}