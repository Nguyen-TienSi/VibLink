using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record FriendshipDetailsResponse : BaseResponse
    {
        public required UserDetailsResponse Requester { get; init; }
        public required UserDetailsResponse Addressee { get; init; }
        public required FriendRequestStatus FriendRequestStatusDto { get; init; }
    }
}
