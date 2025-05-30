using Microsoft.AspNetCore.JsonPatch;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;

namespace VibLink.Services.Internal
{
    public interface IUserDetailsService
    {
        IEnumerable<UserFriendSummaryDto> GetUserFriends();

        IEnumerable<BlockedUserSummaryDto> GetBlockedUsers();

        UserDetailsDto GetUserDetails();

        UserDetailsDto PatchUserDetails(JsonPatchDocument<UserDetails> patchDocument);
    }
}
