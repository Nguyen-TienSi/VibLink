using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class ConversationRepositoryImpl : MongoRepositoryImpl<Conversation>, IConversationRepository
    {
        public ConversationRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Conversation> FindByParticipant(UserDetails userDetails)
        {
            return [.. AsQueryable().Where(c => c.Participants != null && c.Participants.Any(p => p.Id == userDetails.Id))];
        }
    }
}
