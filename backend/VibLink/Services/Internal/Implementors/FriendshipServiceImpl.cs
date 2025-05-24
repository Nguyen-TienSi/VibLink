using AutoMapper;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class FriendshipServiceImpl : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IMapper _mapper;

        public FriendshipServiceImpl(
            IFriendshipRepository friendshipRepository, 
            IMapper mapper
            )
        {
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
        }
    }
}
