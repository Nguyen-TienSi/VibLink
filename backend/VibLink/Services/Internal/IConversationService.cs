using MongoDB.Bson;
using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IConversationService
    {
        IEnumerable<ConversationDetailsDto> GetByParticipant();

        ConversationDetailsDto? GetById(ObjectId id);
    }
}
