using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using VibLink.Helpers;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class MessageServiceImpl : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;
        private readonly HttpContextManager _httpContextManager;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public MessageServiceImpl(
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository,
            IMapper mapper,
            HttpContextManager httpContextManager,
            IUserDetailsRepository userDetailsRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<MessageDetailsResponse?> GetById(ObjectId id)
        {
            var message = await _messageRepository.FindByIdAsync(id);
            if (message == null) return null;

            message.Sender = await _userDetailsRepository.FindByIdAsync(message.SenderId)
                ?? throw new InvalidOperationException("Sender not found.");

            return _mapper.Map<MessageDetailsResponse>(message);
        }

        public async Task<IEnumerable<MessageDetailsResponse>> GetByConversationId(ObjectId conversationId)
        {
            var conversation = await _conversationRepository.FindByIdAsync(conversationId);
            if (conversation == null || conversation.MessageIds == null || !conversation.MessageIds.Any())
                return [];

            var filter = Builders<Message>.Filter.In(m => m.Id, conversation.MessageIds);
            var messages = await _messageRepository.GetMongoCollection().Find(filter).ToListAsync();

            foreach (var message in messages)
            {
                message.Sender = await _userDetailsRepository.FindByIdAsync(message.SenderId)
                    ?? throw new InvalidOperationException("Sender not found.");
            }

            return _mapper.Map<IEnumerable<MessageDetailsResponse>>(messages);
        }

        public async Task<MessageDetailsResponse> InsertOneAsync(MessageCreateRequest messageCreateRequest)
        {
            var senderId = ObjectId.Parse(_httpContextManager.GetUserId());
            var message = _mapper.Map<Message>(
                messageCreateRequest,
                opt => opt.Items["SenderId"] = senderId
            );

            if (!ObjectId.TryParse(messageCreateRequest.ConversationId, out var conversationId))
                throw new ArgumentException("Invalid ConversationId format.");

            await _messageRepository.InsertOneAsync(message);

            var conversation = await _conversationRepository.FindByIdAsync(conversationId);
            if (conversation != null)
            {
                conversation.MessageIds.Add(message.Id);
                await _conversationRepository.ReplaceOneAsync(conversation.Id, conversation);
            }

            message.Sender = await _userDetailsRepository.FindByIdAsync(message.SenderId)
                ?? throw new InvalidOperationException("Sender not found.");

            var messageDetails = _mapper.Map<MessageDetailsResponse>(message);
            return messageDetails;
        }
    }
}
