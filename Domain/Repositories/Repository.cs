using System.Threading.Tasks;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace Domain.Repositories
{
    [UsedImplicitly]
    public sealed class Repository
    {
        /*переменная, которая подключает вас к хосту mongo*/
        // private const string Host =
        //     "mongodb+srv://alexzonic:Flatronw22@cluster0.vvwvz.mongodb.net/MessengerDB?retryWrites=true&w=majority";
        //
        // // это будет именем базы данных, в которой хранятся коллекции
        // private const string DbName = "MessengerDB";

        private IMongoClient _client;
        private IMongoDatabase _database;
        
        // private IMongoClient Client => _client ??= new MongoClient(Host);
        // private IMongoDatabase Database => _database ??= Client.GetDatabase(DbName);

        public Repository(IDataBaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
        }
        
        public IMongoCollection<T> GetCollection<T>([NotNull] string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}