using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IFriendshipService
    {
        public IEnumerable<FriendshipDetailsResponse> GetByAddressee();
    }
}
