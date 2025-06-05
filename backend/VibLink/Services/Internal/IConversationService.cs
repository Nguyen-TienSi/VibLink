using MongoDB.Bson;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IConversationService
    {
        Task<IEnumerable<ConversationDetailsResponse>> GetByParticipant();

        Task<ConversationDetailsResponse?> GetById(ObjectId id);

        Task<ConversationDetailsResponse> InsertOneAsync(ConversationCreateRequest conversationCreateRequest);
    }
}
