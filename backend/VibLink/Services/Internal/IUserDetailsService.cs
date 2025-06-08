using Microsoft.AspNetCore.JsonPatch;
using MongoDB.Bson;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;

namespace VibLink.Services.Internal
{
    public interface IUserDetailsService
    {
        Task<IEnumerable<UserFriendSummaryResponse>> GetUserFriends();

        Task<IEnumerable<BlockedUserSummaryResponse>> GetBlockedUsers();

        Task<UserDetailsResponse?> GetUserDetails();

        Task<UserDetailsResponse> PatchUserDetails(JsonPatchDocument<UserDetails> patchDocument);

        Task<UserSummaryBaseResponse?> GetByEmail(string email);

        Task<IEnumerable<UserFriendSummaryResponse>> GetByConversationId(ObjectId conversationId);
    }
}
