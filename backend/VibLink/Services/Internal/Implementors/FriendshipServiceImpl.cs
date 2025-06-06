using AutoMapper;
using MongoDB.Bson;
using VibLink.Helpers;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;
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

        public async Task<UserSummaryBaseResponse> GetByEmail(string email)
        {
            var userDetails = await _userDetailsRepository.FindByEmailAsync(email) ?? throw new InvalidOperationException("User not found.");
            return _mapper.Map<UserSummaryBaseResponse>(userDetails);
        }

        public async Task<IEnumerable<FriendshipDetailsResponse>> GetByRequesterAsync()
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendships = await _friendshipRepository.FindByRequesterIdAsync(requesterId);

            if (friendships == null || !friendships.Any())
            {
                return [];
            }

            return _mapper.Map<IEnumerable<FriendshipDetailsResponse>>(friendships);
        }

        public async Task<IEnumerable<FriendshipDetailsResponse>> GetByAddresseeAsync()
        {
            var addresseeId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendships = await _friendshipRepository.FindByAddresseeIdAsync(addresseeId);

            if (friendships == null || !friendships.Any())
            {
                return [];
            }

            return _mapper.Map<IEnumerable<FriendshipDetailsResponse>>(friendships);
        }

        public async Task<FriendshipDetailsResponse?> GetByRequesterAsync(ObjectId addresseeId)
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
            {
                return null;
            }

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse?> GetByAddresseeAsync(ObjectId requesterId)
        {
            var addresseeId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
            {
                return null;
            }

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse> InsertByRequesterAsync(ObjectId addresseeId)
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());

            var existingFriendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);
            if (existingFriendship != null)
            {
                return _mapper.Map<FriendshipDetailsResponse>(existingFriendship);
            }

            var friendship = new Friendship
            {
                RequesterId = requesterId,
                AddresseeId = addresseeId,
                FriendshipRequestStatus = Models.Enumerations.FriendshipRequestStatus.PENDING
            };

            await _friendshipRepository.InsertOneAsync(friendship);

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse> UpdateByRequesterAsync(ObjectId addresseeId, Models.DTOs.Shared.FriendshipRequestStatus status)
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
            {
                throw new InvalidOperationException("Friendship not found.");
            }

            friendship.FriendshipRequestStatus = (Models.Enumerations.FriendshipRequestStatus)status;
            await _friendshipRepository.ReplaceOneAsync(friendship.Id, friendship);

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse> UpdateByAddresseeAsync(ObjectId requesterId, Models.DTOs.Shared.FriendshipRequestStatus status)
        {
            var addresseeId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
            {
                throw new InvalidOperationException("Friendship not found.");
            }

            friendship.FriendshipRequestStatus = (Models.Enumerations.FriendshipRequestStatus)status;
            await _friendshipRepository.ReplaceOneAsync(friendship.Id, friendship);

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }
    }
}
