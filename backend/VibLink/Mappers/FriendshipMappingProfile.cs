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
            CreateMap<Friendship, FriendshipDetailsDto>()
                .ForMember(dest => dest.AuditMetadataDto, Opt => Opt.MapFrom(src => src))
                .ForMember(dest => dest.Requester, opt => opt.MapFrom(src => src.Requester))
                .ForMember(dest => dest.Addressee, opt => opt.MapFrom(src => src.Addressee))
                .ForMember(dest => dest.FriendRequestStatusDto, opt => opt.MapFrom(src => (FriendRequestStatusDto)src.FriendRequestStatus));
        }
    }
}
