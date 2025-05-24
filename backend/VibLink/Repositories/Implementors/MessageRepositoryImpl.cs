using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class MessageRepositoryImpl : MongoRepositoryImpl<Message>, IMessageRepository
    {
        public MessageRepositoryImpl(VibLinkDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
