using AutoMapper;
using VibLink.Models.DTOs.Response;
using VibLink.Models.DTOs.Shared;
using VibLink.Models.Entities;

namespace VibLink.Mappers
{
    public class FriendshipMappingProfile : Profile
    {
        public FriendshipMappingProfile()
        {
            CreateMap<Friendship, FriendshipDetailsResponse>()
                .ForMember(dest => dest.AuditMetadataResponse, Opt => Opt.MapFrom(src => src))
                .ForMember(dest => dest.Requester, opt => opt.MapFrom(src => src.Requester))
                .ForMember(dest => dest.Addressee, opt => opt.MapFrom(src => src.Addressee))
                .ForMember(dest => dest.FriendshipRequestStatus, opt => opt.MapFrom(src => (FriendshipRequestStatus)src.FriendshipRequestStatus));
        }
    }
}
