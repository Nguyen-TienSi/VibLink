using AutoMapper;
using MongoDB.Bson;
using VibLink.Helpers;
using VibLink.Models.DTOs.Response;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class FriendshipServiceImpl : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;

        public FriendshipServiceImpl(
            IFriendshipRepository friendshipRepository, 
            IUserDetailsRepository userDetailsRepository,
            IMapper mapper,
            HttpContextManager httpContextManager)
        {
            _friendshipRepository = friendshipRepository;
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
        }

        public IEnumerable<FriendshipDetailsResponse> GetByAddressee()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var addressee = _userDetailsRepository.FindByIdAsync(objectId).Result;
            var friendships = _friendshipRepository.FindByAddressee(addressee!);

            return _mapper.Map<IEnumerable<FriendshipDetailsResponse>>(friendships);
        }
    }
}
