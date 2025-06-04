using Microsoft.AspNetCore.JsonPatch;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;

namespace VibLink.Services.Internal
{
    public interface IUserDetailsService
    {
        IEnumerable<UserFriendSummaryResponse> GetUserFriends();

        IEnumerable<BlockedUserSummaryResponse> GetBlockedUsers();

        UserDetailsResponse GetUserDetails();

        UserDetailsResponse PatchUserDetails(JsonPatchDocument<UserDetails> patchDocument);
    }
}
