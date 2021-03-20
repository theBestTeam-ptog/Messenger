using Api.Helpers;
using Core.Log;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Chats;
using Domain.Repositories.Users;
using JetBrains.Annotations;
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

            Bind<Repository>().ToSelf().InSingletonScope();
                
            Bind<IChatRepository>().To<ChatRepository>().InSingletonScope();
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();

            Bind<IChatHelper>().To<ChatHelper>().InSingletonScope();
            Bind<IUserHelper>().To<UserHelper>().InSingletonScope();

            Bind<IMapper<Chat, ChatDocument>>().To<ChatDocumentMapper>();
            Bind<IMapper<ChatDocument, Chat>>().To<ChatDocumentMapper>();
            
            Bind<IMapper<User, UserDocument>>().To<UserDocumentMapper>();
            Bind<IMapper<UserDocument, User>>().To<UserDocumentMapper>();

            foreach (var binding in Bindings)
            {
                Logger.Info($"implementer for {binding.Service.Name} put into container");
            }
            //Bind(typeof(ILogger<>)).To(typeof(ConsoleLogger<>)).InSingletonScope();
        }
    }
}