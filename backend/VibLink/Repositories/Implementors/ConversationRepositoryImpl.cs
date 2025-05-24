using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class ConversationRepositoryImpl : MongoRepositoryImpl<Conversation>, IConversationRepository
    {
        public ConversationRepositoryImpl(VibLinkDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
