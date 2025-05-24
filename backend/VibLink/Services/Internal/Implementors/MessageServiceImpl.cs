using AutoMapper;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class MessageServiceImpl : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageServiceImpl(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
    }
}
