using AutoMapper;
using MongoDB.Bson;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;

namespace VibLink.Mappers
{
    public class ConversationMappingProfile : Profile
    {
        public ConversationMappingProfile()
        {
            CreateMap<Conversation, ConversationDetailsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.AuditMetadataResponse, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.ChatName))
                .ForMember(dest => dest.ChatPictureUrl, opt => opt.MapFrom(src =>
                    src.ChatPictureId.HasValue ? $"http://localhost:5116/api/filestorage/picture/{src.ChatPictureId}" : string.Empty
                ))
                .ForMember(dest => dest.ConversationType, opt => opt.MapFrom(src => (Models.DTOs.Shared.ConversationType)src.ConversationType));

            CreateMap<ConversationCreateRequest, Conversation>()
                .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.ChatName))
                .ForMember(dest => dest.ConversationType, opt => opt.MapFrom(src => src.ConversationType))
                .AfterMap((src, dest, context) =>
                {
                    context.Items.TryGetValue("UserDetailsId", out var userDetailsIdObj);
                    var userDetailsId = userDetailsIdObj as string;

                    var ids = src.ParticipantIds != null
                        ? src.ParticipantIds.ToList()
                        : [];

                    if (!string.IsNullOrEmpty(userDetailsId) && !ids.Contains(userDetailsId))
                        ids.Add(userDetailsId);

                    dest.ParticipantIds = [.. ids.Select(id => ObjectId.Parse(id))];
                })
                .ForMember(dest => dest.ChatPicture, opt => opt.MapFrom(src => src.ChatPicture))
                .ForMember(dest => dest.ChatPictureId, opt => opt.Ignore())
                .ForMember(dest => dest.Messages, opt => opt.Ignore())
                .ForMember(dest => dest.MessageIds, opt => opt.Ignore());
        }
    }
}
