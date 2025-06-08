namespace VibLink.Models.DTOs.Request
{
    public record FriendshipRequestStatusRequest
    {
        public Models.DTOs.Shared.FriendshipRequestStatus Status { get; init; }
    }
}
