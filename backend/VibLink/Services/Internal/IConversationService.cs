using MongoDB.Bson;
using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IConversationService
    {
        IEnumerable<ConversationDetailsResponse> GetByParticipant();

        ConversationDetailsResponse? GetById(ObjectId id);
    }
}
