using AutoMapper;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;

namespace VibLink.Mappers
{
    public class UserDetailsMappingProfile : Profile
    {
        public UserDetailsMappingProfile()
        {
            CreateMap<UserDetails, UserSummaryBaseResponse>()
                .Include<UserDetails, UserFriendSummaryResponse>()
                .Include<UserDetails, BlockedUserSummaryResponse>();

            CreateMap<UserDetails, UserFriendSummaryResponse>();
            CreateMap<UserDetails, BlockedUserSummaryResponse>();

            CreateMap<UserDetails, UserDetailsResponse>()
                .ForMember(dest => dest.AuditMetadataResponse, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles.Select(role => (VibLink.Models.Enums.UserRole)role)));

            CreateMap<UserRegisterRequest, UserDetails>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .AfterMap((src, dest, context) =>
                {
                    if (context.Items.TryGetValue("PasswordHasher", out var hasherObj) && hasherObj is Func<string, string> hasher)
                    {
                        dest.PasswordHash = hasher(src.Password);
                    }
                    dest.UserRoles = [VibLink.Models.Enums.UserRole.USER];
                    dest.Friends = [];
                    dest.BlockedUsers = [];
                    dest.PictureUrl = string.Empty;
                });
        }
    }
}
