using Core.IoC;
using DataAccess.DbModels;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;

namespace DataAccess.Mappers
{
    [PutInIoC, UsedImplicitly]
    public sealed class UserViewModelMapper : IMapper<UserDocument, UserViewModel>, IMapper<User, UserViewModel>
    {
        public UserViewModel Map(UserDocument source) =>
            source == null
                ? null
                : new UserViewModel
                {
                    Id = source.Id,
                    UserName = source.UserName,
                    Chats = source.Chats,
                    InNetwork = source.InNetwork,
                    ProfileImage = source.ProfileImage,
                };

        public UserViewModel Map(User source) =>
            source == null
                ? null
                : new UserViewModel
                {
                    Id = source.Id,
                    UserName = source.UserName,
                    Chats = source.Chats,
                    InNetwork = source.InNetwork,
                    ProfileImage = source.ProfileImage,
                };
    }
}