using AutoMapper;
using MongoDB.Bson;
using VibLink.Helpers;
using VibLink.Models.DTOs.Response;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class ConversationServiceImpl : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;

        public ConversationServiceImpl(
            IConversationRepository conversationRepository,
            IUserDetailsRepository userDetailsRepository,
            IMapper mapper,
            HttpContextManager httpContextManager)
        {
            _conversationRepository = conversationRepository;
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
        }

        public IEnumerable<ConversationDetailsResponse> GetByParticipant()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userDetails = _userDetailsRepository.FindByIdAsync(objectId).Result;
            var conversations = _conversationRepository.FindByParticipant(userDetails!);

            return _mapper.Map<IEnumerable<ConversationDetailsResponse>>(conversations);
        }

        public ConversationDetailsResponse? GetById(ObjectId id)
        {
            var conversation = _conversationRepository.FindByIdAsync(id).Result;

            return _mapper.Map<ConversationDetailsResponse>(conversation);
        }
    }
}
