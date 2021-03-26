using System;
using System.Collections.Generic;
using Domain.Models;

namespace Api.Test
{
    internal static class ManyUsers
    {
        public static List<User> AllUsers => new List<User>
        {
            User1,
            User2,
            User3,
            User4,
            User5,
            User6,
            User7,
            User8,
            User9,
            User10,
            User11,
            User12,
            User13,
            User14,
            User15,
            User16,
            User17,
        };
        
        private static readonly User User1 = new User
        {
            Id = new Guid(),
            UserName = "katya",
            Login = "katya",
            Password = "katya",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };        
        
        private static readonly User User2 = new User
        {
            Id = new Guid(),
            UserName = "ulya",
            Login = "ulya",
            Password = "ulya",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };

        private static readonly User User3 = new User
        {
            Id = new Guid(),
            UserName = "lena",
            Login = "lena",
            Password = "lena",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User4 = new User
        {
            Id = new Guid(),
            UserName = "kathrine",
            Login = "kathrine",
            Password = "kathrine",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User5 = new User
        {
            Id = new Guid(),
            UserName = "lilya",
            Login = "lilya",
            Password = "lilya",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User6 = new User
        {
            Id = new Guid(),
            UserName = "iluha",
            Login = "iluha",
            Password = "iluha",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User7 = new User
        {
            Id = new Guid(),
            UserName = "inna",
            Login = "inna",
            Password = "inna",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User8 = new User
        {
            Id = new Guid(),
            UserName = "alex",
            Login = "alex",
            Password = "alex",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User9 = new User
        {
            Id = new Guid(),
            UserName = "anna",
            Login = "anna",
            Password = "anna",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User10 = new User
        {
            Id = new Guid(),
            UserName = "anton",
            Login = "anton",
            Password = "anton",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User11 = new User
        {
            Id = new Guid(),
            UserName = "artem",
            Login = "artem",
            Password = "artem",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User12 = new User
        {
            Id = new Guid(),
            UserName = "nadya",
            Login = "nadya",
            Password = "nadya",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User13 = new User
        {
            Id = new Guid(),
            UserName = "natasha",
            Login = "natasha",
            Password = "natasha",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User14 = new User
        {
            Id = new Guid(),
            UserName = "andrey",
            Login = "andrey",
            Password = "andrey",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User15 = new User
        {
            Id = new Guid(),
            UserName = "nikita",
            Login = "nikita",
            Password = "nikita",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User16 = new User
        {
            Id = new Guid(),
            UserName = "nikolay",
            Login = "nikolay",
            Password = "nikolay",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
        
        private static readonly User User17 = new User
        {
            Id = new Guid(),
            UserName = "kolya",
            Login = "kolya",
            Password = "kolya",
            Chats = new List<string>(),
            Private = false,
            Registration = DateTime.Now,
            Authorize = DateTime.Now,
            ProfileImage = null,
            InNetwork = true,
        };
    }
}