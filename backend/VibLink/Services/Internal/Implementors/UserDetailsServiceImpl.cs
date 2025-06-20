﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MongoDB.Bson;
using System.Threading.Tasks;
using VibLink.Helpers;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class UserDetailsServiceImpl : IUserDetailsService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;

        public UserDetailsServiceImpl(
            IUserDetailsRepository userDetailsRepository,
            IConversationRepository conversationRepository,
            IMapper mapper,
            HttpContextManager httpContextManager)
        {
            _userDetailsRepository = userDetailsRepository;
            _conversationRepository = conversationRepository;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
        }

        public async Task<IEnumerable<UserFriendSummaryResponse>> GetUserFriends()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userFriends = await _userDetailsRepository.FindUserFriendsAsync(objectId);

            return _mapper.Map<IEnumerable<UserFriendSummaryResponse>>(userFriends);
        }

        public async Task<IEnumerable<BlockedUserSummaryResponse>> GetBlockedUsers()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var blockedUsers = await _userDetailsRepository.FindBlockedUsersAsync(objectId);
            return _mapper.Map<IEnumerable<BlockedUserSummaryResponse>>(blockedUsers);
        }

        public async Task<UserDetailsResponse?> GetUserDetails()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userDetails = await _userDetailsRepository.FindByIdAsync(objectId);

            return _mapper.Map<UserDetailsResponse>(userDetails);
        }

        public async Task<UserDetailsResponse> PatchUserDetails(JsonPatchDocument<UserDetails> patchDocument)
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userDetails = await _userDetailsRepository.FindByIdAsync(objectId);

            patchDocument.ApplyTo(userDetails!);

            await _userDetailsRepository.ReplaceOneAsync(objectId, userDetails!);

            return _mapper.Map<UserDetailsResponse>(userDetails);
        }

        public async Task<UserSummaryBaseResponse?> GetByEmail(string email)
        {
            var userDetails = await _userDetailsRepository.FindByEmailAsync(email);

            if (userDetails == null)
            {
                return null;
            }

            return _mapper.Map<UserSummaryBaseResponse>(userDetails);
        }

        public async Task<IEnumerable<UserFriendSummaryResponse>> GetByConversationId(ObjectId conversationId)
        {
            var conversation = await _conversationRepository.FindByIdAsync(conversationId);
            if (conversation == null || conversation.ParticipantIds == null || conversation.ParticipantIds.Count == 0)
                return [];

            var users = new List<UserDetails>();
            foreach (var participantId in conversation.ParticipantIds)
            {
                var user = await _userDetailsRepository.FindByIdAsync(participantId);
                if (user != null)
                    users.Add(user);
            }

            return _mapper.Map<ICollection<UserFriendSummaryResponse>>(users);
        }
    }
}
