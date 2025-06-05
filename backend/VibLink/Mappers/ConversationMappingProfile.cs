using AutoMapper;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Models.DTOs.Shared;
using VibLink.Models.Entities;
using VibLink.Mappers.Converters;

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
                .ForMember(dest => dest.ChatPictureUrl, opt => opt.MapFrom(src =>
                    src.ChatPictureId.HasValue ? $"/api/filestorage/picture/{src.ChatPictureId}" : string.Empty
                ))
                .ForMember(dest => dest.ConversationTypeDto, opt => opt.MapFrom(src => (ConversationType)src.ConversationType))
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<ConversationCreateRequest, Conversation>()
                .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.ChatName))
                .ForMember(dest => dest.ConversationType, opt => opt.MapFrom(src => src.ConversationType))
                .ForMember(dest => dest.ParticipantIds, opt => opt.MapFrom(src => src.ParticipantIds))
                .ForMember(dest => dest.ChatPicture, opt => opt.MapFrom(src => src.ChatPicture))
                .ForMember(dest => dest.ChatPictureId, opt => opt.Ignore())
                .ForMember(dest => dest.Messages, opt => opt.Ignore())
                .ForMember(dest => dest.MessageIds, opt => opt.Ignore());
        }
    }
}
