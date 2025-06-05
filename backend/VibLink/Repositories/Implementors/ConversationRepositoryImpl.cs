using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class ConversationRepositoryImpl : MongoRepositoryImpl<Conversation>, IConversationRepository
    {
        public ConversationRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Conversation>> FindByParticipantId(ObjectId objectId)
        {
            var filter = Builders<Conversation>.Filter.AnyEq(c => c.ParticipantIds, objectId);
            var conversations = await _mongoCollection.Find(filter).ToListAsync();
            return conversations;
        }
    }
}
