using Core.Settings;
using Domain;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    [UsedImplicitly]
    public sealed class Repository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;

        private IDataBaseSettings _settings;
        
        public Repository(IDataBaseSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(_settings.ConnectionString);
            _database = _client.GetDatabase(_settings.DatabaseName);
        }
        
        public IMongoCollection<T> GetCollection<T>([NotNull] string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}