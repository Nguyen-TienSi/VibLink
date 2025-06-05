using AutoMapper;
using MongoDB.Bson;
using System.Threading.Tasks;
using VibLink.Helpers;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class ConversationServiceImpl : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;

        public ConversationServiceImpl(
            IConversationRepository conversationRepository,
            IUserDetailsRepository userDetailsRepository,
            IFileStorageRepository fileStorageRepository,
            IMapper mapper,
            HttpContextManager httpContextManager)
        {
            _conversationRepository = conversationRepository;
            _userDetailsRepository = userDetailsRepository;
            _fileStorageRepository = fileStorageRepository;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
        }

        public async Task<ConversationDetailsResponse?> GetById(ObjectId id)
        {
            var conversation = await _conversationRepository.FindByIdAsync(id);

            if (conversation == null)
            {
                return null;
            }

            return _mapper.Map<ConversationDetailsResponse>(conversation);
        }

        public async Task<IEnumerable<ConversationDetailsResponse>> GetByParticipant()
        {
            var objectId = ObjectId.Parse(_httpContextManager.GetUserId());

            var conversations = await _conversationRepository.FindByParticipantId(objectId);

            return _mapper.Map<IEnumerable<ConversationDetailsResponse>>(conversations);
        }

        public async Task<ConversationDetailsResponse> InsertOneAsync(ConversationCreateRequest conversationCreateRequest)
        {
            var conversation = _mapper.Map<Models.Entities.Conversation>(conversationCreateRequest);

            if (conversationCreateRequest.ChatPicture != null)
            {
                var fileStorage = _mapper.Map<Models.Entities.FileStorage>(conversationCreateRequest.ChatPicture);
                await _fileStorageRepository.InsertOneAsync(fileStorage);
                conversation.ChatPictureId = fileStorage.Id;
            }

            await _conversationRepository.InsertOneAsync(conversation);

            var response = _mapper.Map<ConversationDetailsResponse>(conversation);
            return response;
        }
    }
}
