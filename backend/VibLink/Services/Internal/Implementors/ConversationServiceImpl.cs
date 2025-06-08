using AutoMapper;
using MongoDB.Bson;
using System.Threading.Tasks;
using VibLink.Helpers;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class ConversationServiceImpl : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;

        public ConversationServiceImpl(
            IConversationRepository conversationRepository,
            IUserDetailsRepository userDetailsRepository,
            IFileStorageRepository fileStorageRepository,
            IMessageRepository messageRepository,
            IMapper mapper,
            HttpContextManager httpContextManager)
        {
            _conversationRepository = conversationRepository;
            _userDetailsRepository = userDetailsRepository;
            _fileStorageRepository = fileStorageRepository;
            _messageRepository = messageRepository;
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

            // Populate Participants
            conversation.Participants = new List<UserDetails>();
            foreach (var participantId in conversation.ParticipantIds)
            {
                var user = await _userDetailsRepository.FindByIdAsync(participantId);
                if (user != null)
                    conversation.Participants.Add(user);
            }

            // Populate Messages
            conversation.Messages = new List<Message>();
            foreach (var messageId in conversation.MessageIds)
            {
                var message = await _messageRepository.FindByIdAsync(messageId);
                if (message != null)
                    conversation.Messages.Add(message);
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
            var userDetailsId = _httpContextManager.GetUserId();
            var conversation = _mapper.Map<Conversation>(conversationCreateRequest, opt => opt.Items["UserDetailsId"] = userDetailsId);

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
