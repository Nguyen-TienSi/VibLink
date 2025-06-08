using MongoDB.Bson;
using VibLink.Models.DTOs.Response;
using VibLink.Models.DTOs.Request;

namespace VibLink.Services.Internal
{
    public interface IFriendshipService
    {
        Task<UserSummaryBaseResponse> GetByEmail(string email);

        Task<IEnumerable<FriendshipDetailsResponse>> GetPendingInvitesAsync();

        Task<FriendshipDetailsResponse?> GetByRequesterAsync(ObjectId addresseeId);

        Task<FriendshipDetailsResponse?> GetByAddresseeAsync(ObjectId requesterId);

        Task<FriendshipDetailsResponse> InsertByRequesterAsync(ObjectId addresseeId);

        Task<FriendshipDetailsResponse> UpdateByRequesterAsync(ObjectId addresseeId, FriendshipRequestStatusRequest friendshipRequestStatusRequest);

        Task<FriendshipDetailsResponse> UpdateByAddresseeAsync(ObjectId requesterId, FriendshipRequestStatusRequest friendshipRequestStatusRequest);
    }
}
