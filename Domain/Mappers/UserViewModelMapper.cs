using Domain.DbModels;
using Domain.Models;
using JetBrains.Annotations;

namespace Domain.Mappers
{
    [UsedImplicitly]
    public sealed class UserViewModelMapper : IMapper<UserDocument, UserViewModel>, IMapper<User, UserViewModel>
    {
        public UserViewModel Map(UserDocument source) =>
            source == null
                ? null
                : new UserViewModel
                {
                    Id = source.Id,
                    UserName = source.UserName,
                    ChatsIds = source.ChatsIds,
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
                    ChatsIds = source.ChatsIds,
                    InNetwork = source.InNetwork,
                    ProfileImage = source.ProfileImage,
                };
    }
}