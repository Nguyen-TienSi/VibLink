using AutoMapper;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class ConversationServiceImpl : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;

        public ConversationServiceImpl(
            IConversationRepository conversationRepository,
            IMapper mapper
            )
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }
    }
}
