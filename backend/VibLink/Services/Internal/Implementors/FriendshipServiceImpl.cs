using AutoMapper;
using MongoDB.Bson;
using VibLink.Helpers;
using VibLink.Models.DTOs.Request;
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

        public async Task<FriendshipDetailsResponse?> GetByRequesterAsync(ObjectId addresseeId)
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
                return null;

            friendship.Requester = await _userDetailsRepository.FindByIdAsync(friendship.RequesterId)
                ?? throw new InvalidOperationException("Requester not found.");
            friendship.Addressee = await _userDetailsRepository.FindByIdAsync(friendship.AddresseeId)
                ?? throw new InvalidOperationException("Addressee not found.");

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse?> GetByAddresseeAsync(ObjectId requesterId)
        {
            var addresseeId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
                return null;

            friendship.Requester = await _userDetailsRepository.FindByIdAsync(friendship.RequesterId)
                ?? throw new InvalidOperationException("Requester not found.");
            friendship.Addressee = await _userDetailsRepository.FindByIdAsync(friendship.AddresseeId)
                ?? throw new InvalidOperationException("Addressee not found.");

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse> InsertByRequesterAsync(ObjectId addresseeId)
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());

            var existingFriendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);
            if (existingFriendship != null)
            {
                existingFriendship.Requester = await _userDetailsRepository.FindByIdAsync(existingFriendship.RequesterId)
                    ?? throw new InvalidOperationException("Requester not found.");
                existingFriendship.Addressee = await _userDetailsRepository.FindByIdAsync(existingFriendship.AddresseeId)
                    ?? throw new InvalidOperationException("Addressee not found.");
                return _mapper.Map<FriendshipDetailsResponse>(existingFriendship);
            }

            var friendship = new Friendship
            {
                RequesterId = requesterId,
                AddresseeId = addresseeId,
                FriendshipRequestStatus = Models.Enumerations.FriendshipRequestStatus.PENDING
            };

            await _friendshipRepository.InsertOneAsync(friendship);

            friendship.Requester = await _userDetailsRepository.FindByIdAsync(friendship.RequesterId)
                ?? throw new InvalidOperationException("Requester not found.");
            friendship.Addressee = await _userDetailsRepository.FindByIdAsync(friendship.AddresseeId)
                ?? throw new InvalidOperationException("Addressee not found.");

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse> UpdateByRequesterAsync(ObjectId addresseeId, FriendshipRequestStatusRequest friendshipRequestStatusRequest)
        {
            var requesterId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
                throw new InvalidOperationException("Friendship not found.");

            var newStatus = (Models.Enumerations.FriendshipRequestStatus)friendshipRequestStatusRequest.Status;
            friendship.FriendshipRequestStatus = newStatus;
            await _friendshipRepository.ReplaceOneAsync(friendship.Id, friendship);

            // If accepted, update both users' FriendIds
            if (newStatus == Models.Enumerations.FriendshipRequestStatus.ACCEPTED)
            {
                var requester = await _userDetailsRepository.FindByIdAsync(requesterId)
                    ?? throw new InvalidOperationException("Requester not found.");
                var addressee = await _userDetailsRepository.FindByIdAsync(addresseeId)
                    ?? throw new InvalidOperationException("Addressee not found.");

                if (!requester.FriendIds.Contains(addresseeId))
                {
                    requester.FriendIds.Add(addresseeId);
                    await _userDetailsRepository.ReplaceOneAsync(requester.Id, requester);
                }
                if (!addressee.FriendIds.Contains(requesterId))
                {
                    addressee.FriendIds.Add(requesterId);
                    await _userDetailsRepository.ReplaceOneAsync(addressee.Id, addressee);
                }
            }

            friendship.Requester = await _userDetailsRepository.FindByIdAsync(friendship.RequesterId)
                ?? throw new InvalidOperationException("Requester not found.");
            friendship.Addressee = await _userDetailsRepository.FindByIdAsync(friendship.AddresseeId)
                ?? throw new InvalidOperationException("Addressee not found.");

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<FriendshipDetailsResponse> UpdateByAddresseeAsync(ObjectId requesterId, FriendshipRequestStatusRequest friendshipRequestStatusRequest)
        {
            var addresseeId = ObjectId.Parse(_httpContextManager.GetUserId());
            var friendship = await _friendshipRepository.FindByRequesterIdAndAddresseeIdAsync(requesterId, addresseeId);

            if (friendship == null)
                throw new InvalidOperationException("Friendship not found.");

            var newStatus = (Models.Enumerations.FriendshipRequestStatus)friendshipRequestStatusRequest.Status;
            friendship.FriendshipRequestStatus = newStatus;
            await _friendshipRepository.ReplaceOneAsync(friendship.Id, friendship);

            // If accepted, update both users' FriendIds
            if (newStatus == Models.Enumerations.FriendshipRequestStatus.ACCEPTED)
            {
                var requester = await _userDetailsRepository.FindByIdAsync(requesterId)
                    ?? throw new InvalidOperationException("Requester not found.");
                var addressee = await _userDetailsRepository.FindByIdAsync(addresseeId)
                    ?? throw new InvalidOperationException("Addressee not found.");

                if (!requester.FriendIds.Contains(addresseeId))
                {
                    requester.FriendIds.Add(addresseeId);
                    await _userDetailsRepository.ReplaceOneAsync(requester.Id, requester);
                }
                if (!addressee.FriendIds.Contains(requesterId))
                {
                    addressee.FriendIds.Add(requesterId);
                    await _userDetailsRepository.ReplaceOneAsync(addressee.Id, addressee);
                }
            }

            friendship.Requester = await _userDetailsRepository.FindByIdAsync(friendship.RequesterId)
                ?? throw new InvalidOperationException("Requester not found.");
            friendship.Addressee = await _userDetailsRepository.FindByIdAsync(friendship.AddresseeId)
                ?? throw new InvalidOperationException("Addressee not found.");

            return _mapper.Map<FriendshipDetailsResponse>(friendship);
        }

        public async Task<IEnumerable<FriendshipDetailsResponse>> GetPendingInvitesAsync()
        {
            var userId = ObjectId.Parse(_httpContextManager.GetUserId());

            var sent = await _friendshipRepository.FindPendingByRequesterIdAsync(userId);
            var received = await _friendshipRepository.FindPendingByAddresseeIdAsync(userId);

            var all = sent.Concat(received).ToList();

            foreach (var friendship in all)
            {
                friendship.Requester = await _userDetailsRepository.FindByIdAsync(friendship.RequesterId)
                    ?? throw new InvalidOperationException("Requester not found.");
                friendship.Addressee = await _userDetailsRepository.FindByIdAsync(friendship.AddresseeId)
                    ?? throw new InvalidOperationException("Addressee not found.");
            }

            return _mapper.Map<IEnumerable<FriendshipDetailsResponse>>(all);
        }
    }
}
