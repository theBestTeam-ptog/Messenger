using Domain.Repositories.Chats;
using JetBrains.Annotations;

namespace Api.Helpers
{
    [UsedImplicitly]
    public sealed class ChatHelper : IChatHelper
    {
        private IChatRepository _db;

        public ChatHelper(IChatRepository db)
        {
            _db = db;
        }
    }
}