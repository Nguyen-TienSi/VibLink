using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record FriendshipDetailsDto : BaseResponseDto
    {
        public required UserDetailsDto Requester { get; init; }
        public required UserDetailsDto Addressee { get; init; }
        public required FriendRequestStatusDto FriendRequestStatusDto { get; init; }
    }
}
