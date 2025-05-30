using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IConversationRepository : IMongoRepository<Conversation>
    {
        public IEnumerable<Conversation> FindByParticipant(UserDetails userDetails);
    }
}
