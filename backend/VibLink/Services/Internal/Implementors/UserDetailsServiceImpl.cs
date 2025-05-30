using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MongoDB.Bson;
using VibLink.Http;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class UserDetailsServiceImpl : IUserDetailsService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;

        public UserDetailsServiceImpl(
            IUserDetailsRepository userDetailsRepository,
            IMapper mapper,
            HttpContextManager httpContextManager)
        {
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
        }

        public IEnumerable<UserFriendSummaryDto> GetUserFriends()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userFriends = _userDetailsRepository.FindUserFriends(objectId);

            return _mapper.Map<IEnumerable<UserFriendSummaryDto>>(userFriends);
        }

        public IEnumerable<BlockedUserSummaryDto> GetBlockedUsers()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var blockedUsers = _userDetailsRepository.FindBlockedUsers(objectId);
            return _mapper.Map<IEnumerable<BlockedUserSummaryDto>>(blockedUsers);
        }

        public UserDetailsDto GetUserDetails()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userDetails = _userDetailsRepository.FindByIdAsync(objectId).Result;

            return _mapper.Map<UserDetailsDto>(userDetails);
        }

        public UserDetailsDto PatchUserDetails(JsonPatchDocument<UserDetails> patchDocument)
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userDetails = _userDetailsRepository.FindByIdAsync(objectId).Result;

            patchDocument.ApplyTo(userDetails!);

            _userDetailsRepository.ReplaceOneAsync(objectId, userDetails!);

            return _mapper.Map<UserDetailsDto>(userDetails);
        }
    }
}
