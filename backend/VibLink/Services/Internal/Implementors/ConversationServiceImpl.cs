using AutoMapper;
using MongoDB.Bson;
using VibLink.Http;
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

        public IEnumerable<ConversationDetailsDto> GetByParticipant()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());
            var userDetails = _userDetailsRepository.FindByIdAsync(objectId).Result;
            var conversations = _conversationRepository.FindByParticipant(userDetails!);

            return _mapper.Map<IEnumerable<ConversationDetailsDto>>(conversations);
        }

        public ConversationDetailsDto? GetById(ObjectId id)
        {
            var conversation = _conversationRepository.FindByIdAsync(id).Result;

            return _mapper.Map<ConversationDetailsDto>(conversation);
        }
    }
}
