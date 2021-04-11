using Core.IoC;
using Domain.Repositories;
using JetBrains.Annotations;

namespace Api.Helpers
{
    [PutInIoC, UsedImplicitly]
    public sealed class ChatHelper : IChatHelper
    {
        private IChatRepository _db;

        public ChatHelper(IChatRepository db)
        {
            _db = db;
        }
    }
}