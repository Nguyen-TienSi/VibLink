using MongoDB.Bson;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDetailsResponse>> GetByConversationId(ObjectId conversationId);

        Task<MessageDetailsResponse> InsertOneAsync(MessageCreateRequest messageCreateRequest);
    }
}
