using MongoDB.Bson;
using VibLink.Models.DTOs.Response;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Services.Internal
{
    public interface IFriendshipService
    {
        Task<UserSummaryBaseResponse> GetByEmail(string email);

        Task<IEnumerable<FriendshipDetailsResponse>> GetByRequesterAsync();

        Task<IEnumerable<FriendshipDetailsResponse>> GetByAddresseeAsync();

        Task<FriendshipDetailsResponse?> GetByRequesterAsync(ObjectId addresseeId);

        Task<FriendshipDetailsResponse?> GetByAddresseeAsync(ObjectId requesterId);

        Task<FriendshipDetailsResponse> InsertByRequesterAsync(ObjectId addresseeId);

        Task<FriendshipDetailsResponse> UpdateByRequesterAsync(ObjectId addresseeId, FriendRequestStatus status);

        Task<FriendshipDetailsResponse> UpdateByAddresseeAsync(ObjectId requesterId, FriendRequestStatus status);
    }
}
