using Api.Helpers;
using Core.Log;
using Domain;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Chats;
using Domain.Repositories.Users;
using JetBrains.Annotations;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Core
{
    [UsedImplicitly]
    public sealed class Registry : NinjectModule
    {
        private static readonly ILogger<Registry> Logger = new ConsoleLogger<Registry>();
        
        public override void Load()
        {
            /*this.Bind(f =>
            {
                var mappers = f
                    .FromThisAssembly()
                    .Select(typeof(IMapper<,>).IsAssignableFrom);

                mappers.BindAllInterfaces();
            });*/ 
            //todo хз почему не работает код выше, поэтому мапперы ручками биндить придется

            Bind<IDataBaseSettings>().To<DataBaseSettings>()
                .WithPropertyValue("ConnectionString", "mongodb://localhost:27017")
                .WithPropertyValue("DatabaseName", "MessengerDB");

            Bind<Repository>().ToSelf().InSingletonScope();
                
            Bind<IChatRepository>().To<ChatRepository>().InSingletonScope();
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();

            Bind<IChatHelper>().To<ChatHelper>().InSingletonScope();
            Bind<IUserHelper>().To<UserHelper>().InSingletonScope();

            Bind<IDuplexMapper<Chat, ChatDocument>>().To<ChatDocumentMapper>().InSingletonScope();
            Bind<IDuplexMapper<User, UserDocument>>().To<UserDocumentMapper>().InSingletonScope();

            Bind<IMapper<User, UserViewModel>, IMapper<UserDocument, UserViewModel>>().To<UserViewModelMapper>();

            foreach (var binding in Bindings)
            {
                Logger.Info($"implementer for {binding.Service.Name} put into container");
            }
        }
    }
}