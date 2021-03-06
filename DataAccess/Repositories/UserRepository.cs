using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Constants;
using Domain.Protos;
using Domain.Repositories;
using Google.Protobuf.WellKnownTypes;
using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    [PutInIoC, UsedImplicitly]
    public sealed class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        private IMongoCollection<UserDocument> Users => _repository.GetCollection<UserDocument>(CollectionsNames.Users);

        public UserRepository(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = await Users
                .Find(new BsonDocument("_id", id))
                .FirstOrDefaultAsync();

            return _mapper.Map<User>(user);
        }

        public async Task<bool> CheckLoginAsync(string login)
        {
            return await Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.Login, login))
                .FirstOrDefaultAsync() is null;
        }

        public async Task<User> GetUserValidationAsync(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login)) return null;

            var user = await Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.Login, login))
                .FirstOrDefaultAsync();

            return _mapper.Map<User>(UserIsValidAsync(user, password));
        }

        public async Task CreateUserAsync(string userName, string login, string password)
        {
            var user = new User
            {
                Authorize = Timestamp.FromDateTime(DateTime.UtcNow),
                HasImage = false,
                Id = Guid.NewGuid().ToString(),
                InNetwork = true,
                Login = login,
                Password = password,
                Private = false,
                Registration = Timestamp.FromDateTime(DateTime.UtcNow),
                UserName = userName
            };
            
            await Users.InsertOneAsync(_mapper.Map<UserDocument>(user));
        }

        public IEnumerable<User> GetUsersByNameAsync(string name)
        {
            return Users
                .Find(Builders<UserDocument>.Filter.Eq(u => u.UserName, name))
                .ToEnumerable()
                .Select(_mapper.Map<User>);
        }

        [ItemCanBeNull]
        public IEnumerable<User> SearchSync(string suggest) =>
            Users.Find(Builders<UserDocument>.Filter.Where(
                        u => u.UserName.StartsWith(suggest)
                    )
                )
                .ToEnumerable()
                .Select(_mapper.Map<User>);

        /*
         todo для метода выше
         если для сервиса нужен именно асинхронный, то использовать это ->
         return Task.Factory.StartNew(() =>
             {
                 return Users.Find(Builders<UserDocument>.Filter.Where(
                             u => u.UserName.StartsWith(suggest)
                     )
                     .ToEnumerable()
                     .Select(_mapper.Map<User>);
             });
        todo не забыть переименовать в SearchAsync и поменять возвращаемый тип тут и в интерфейсе на Task<IEnumerable<User>>
         */

        private UserDocument UserIsValidAsync(UserDocument user, string password)
        {
            return user != null && user.Password.Equals(password) ? user : null;
        }
    }
}