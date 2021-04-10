using Core.Settings;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    [UsedImplicitly]
    public sealed class Repository
    {
        /*переменная, которая подключает вас к хосту mongo*/
        private const string Host =
            "mongodb+srv://alexzonic:Flatronw22@cluster0.vvwvz.mongodb.net/MessengerDB?retryWrites=true&w=majority";

        private readonly IConfigurationHelper _configurationHelper;
        
        public Repository(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }
        
        // это будет именем базы данных, в которой хранятся коллекции
        private const string DbName = "MessengerDB";

        private IMongoClient _client;
        private IMongoDatabase _database;
        
        private IMongoClient Client => _client ??= new MongoClient(_configurationHelper.GetDbHost());
        private IMongoDatabase Database => _database ??= Client.GetDatabase(_configurationHelper.GetDbName());

        public IMongoCollection<T> GetCollection<T>([NotNull] string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }
    }
}