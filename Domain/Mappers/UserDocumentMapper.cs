using Domain.DbModels;
using Domain.Models;
using JetBrains.Annotations;

namespace Domain.Mappers
{
    [UsedImplicitly]
    public sealed class UserDocumentMapper : IMapper<User, UserDocument>, IMapper<UserDocument, User>
    {
        [CanBeNull]
        public UserDocument Map([CanBeNull] User source) => source is null 
            ? null 
            : new UserDocument
            {
                Id = source.Id,
                UserName = source.UserName,
                Login = source.Login,
                Password = source.Password,
                Chats = source.Chats,
                ProfileImage = source.ProfileImage,
                Private = source.Private,
                Authorize = source.Authorize,
                Registration = source.Registration,
                InNetwork = source.InNetwork,
            };

        [CanBeNull]
        public User Map([CanBeNull] UserDocument source) => source is null
            ? null
            : new User
            {
                Id = source.Id,
                UserName = source.UserName,
                Login = source.Login,
                Password = source.Password,
                Chats = source.Chats,
                ProfileImage = source.ProfileImage,
                Private = source.Private,
                Authorize = source.Authorize,
                Registration = source.Registration,
                InNetwork = source.InNetwork,
            };
    }
}