using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IConversationRepository : IMongoRepository<Conversation>
    {
        Task<IEnumerable<Conversation>> FindByParticipantId(ObjectId objectId);
    }
}
