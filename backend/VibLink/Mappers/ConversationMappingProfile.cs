using AutoMapper;
using VibLink.Models.DTOs.Response;
using VibLink.Models.DTOs.Shared;
using VibLink.Models.Entities;

namespace VibLink.Mappers
{
    public class ConversationMappingProfile : Profile
    {
        public ConversationMappingProfile()
        {
            CreateMap<Conversation, ConversationDetailsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuditMetadataResponse, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.ChatName))
                .ForMember(dest => dest.ChatPictureUrl, opt => opt.MapFrom(src => src.ChatPictureUrl))
                .ForMember(dest => dest.ConversationTypeDto, opt => opt.MapFrom(src => (ConversationType)src.ConversationType))
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));
        }
    }
}
