using AutoMapper;
using VibLink.Models.DTOs.Response;
using VibLink.Models.DTOs.Shared;
using VibLink.Models.Entities;

namespace VibLink.Mappers
{
    public class UserDetailsMappingProfile : Profile
    {
        public UserDetailsMappingProfile()
        {
            CreateMap<UserDetails, UserSummaryBaseDto>()
                .Include<UserDetails, UserFriendSummaryDto>()
                .Include<UserDetails, BlockedUserSummaryDto>();

            CreateMap<UserDetails, UserFriendSummaryDto>();

            CreateMap<UserDetails, BlockedUserSummaryDto>();

            CreateMap<UserDetails, UserDetailsDto>()
            .ForMember(dest => dest.AuditMetadataDto, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles.Select(role => (UserRoleDto)role)));
        }
    }
}
