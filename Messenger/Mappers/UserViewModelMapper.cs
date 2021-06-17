using System;
using System.IO;
using System.Windows.Controls;
using Core.IoC;
using DataAccess.DbModels;
using Domain.Mappers;
using Domain.Models;
using JetBrains.Annotations;
using Messenger.ViewModels;
using System.Drawing;

namespace Messenger.Mappers
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
                    InNetwork = source.InNetwork,
                    ProfileImage = source.ProfileImage,
                };

        private Image ConverterImage(string imageString)
        {
            var bytes = Convert.FromBase64String(imageString);

            Image image;
            using var ms = new MemoryStream(bytes);
            image = Image.FromStream(ms);

            return image;
        }
    }
    
    
}