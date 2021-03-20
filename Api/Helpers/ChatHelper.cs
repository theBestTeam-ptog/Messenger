using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories.Chats;
using JetBrains.Annotations;

namespace Api.Helpers
{
    public class ChatHelper
    {
        private IChatRepository _db;

        public ChatHelper(IChatRepository db)
        {
            _db = db;
        }
    }
}