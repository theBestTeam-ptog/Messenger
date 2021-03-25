using System.Collections.Generic;
using System.Linq;
using Api.Helpers;
using Core;
using Domain.Mappers;
using Domain.Models;
using Ninject;
using NUnit.Framework;

namespace Api.Test
{
    [TestFixture]
    internal sealed class ConnectToDb : Program
    {
        private readonly IKernel _container; 
        
        private readonly IUserHelper _userHelper;
        private readonly IMapper<User, UserViewModel> _userViewModelMapper;

        public ConnectToDb()
        {
            _container = new StandardKernel(new Registry());
            _userHelper = _container.Get<IUserHelper>();
            _userViewModelMapper = _container.Get<IMapper<User, UserViewModel>>();
        }

        // ТЕСТ НЕ АКТУАЛЕН, Т.К. ДАННЫЕ В БД ИЗМЕНЕНЫ
        [Test]
        public void AddDocuments()
        {
            var users = ManyUsers.AllUsers;
         
            //-----ЗАПУСКАТЬ ЦИКЛ ОДИН РАЗ ДЛЯ ЗАПОЛНЕНИЯ БД, ПОТОМ КОММЕНТИТЬ ЕГО---------
            foreach (var user in users)
            {
                _userHelper.RegisterUser(user).GetAwaiter();
            }
            //-----------------------------------------------------------------------------
            
            var kirill = _userHelper.Search("Kirill").FirstOrDefault();
            var ilya = _userHelper.Search("Ilya").FirstOrDefault();
            var leha = _userHelper.Search("Leha").FirstOrDefault();
            
            var expectUsers = users.Select(_userViewModelMapper.Map).ToList();
            var actualUsers = new List<UserViewModel> {kirill, ilya, leha};
            
            Assert.AreEqual(expectUsers.Count, actualUsers.Count);
            if (expectUsers.Count == actualUsers.Count)
            {
                for (var i = 0; i < expectUsers.Count; i++)
                {
                    CheckUserViewModel(expectUsers[i], actualUsers[i]);
                }
            }
        }

        private void CheckUserViewModel(UserViewModel expectUser, UserViewModel actualUser)
        {
            //Assert.AreEqual(expectUser.Id, actualUser.Id);
            Assert.AreEqual(expectUser.UserName, actualUser.UserName);
            Assert.AreEqual(expectUser.ProfileImage, actualUser.ProfileImage);
            Assert.AreEqual(expectUser.Chats.Count, actualUser.Chats.Count);
            Assert.AreEqual(expectUser.InNetwork, actualUser.InNetwork);
        }
    }
}