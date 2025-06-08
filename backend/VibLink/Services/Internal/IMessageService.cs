using MongoDB.Bson;
using System.Collections.Generic;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IMessageService
    {
        Task<MessageDetailsResponse?> GetById(ObjectId id);

        Task<IEnumerable<MessageDetailsResponse>> GetByConversationId(ObjectId conversationId);

        Task<MessageDetailsResponse> InsertOneAsync(MessageCreateRequest messageCreateRequest);
    }
}
