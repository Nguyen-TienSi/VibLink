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
                .Include<UserDetails, BlockedUserSummaryResponse>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src =>
                    src.Picture != null ? $"/api/filestorage/{src.Picture.Id}" : string.Empty
                ));

            CreateMap<UserDetails, UserFriendSummaryResponse>();

            CreateMap<UserDetails, BlockedUserSummaryResponse>();

            CreateMap<UserDetails, UserDetailsResponse>()
                .ForMember(dest => dest.AuditMetadataResponse, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles.Select(role => (VibLink.Models.Enumerations.UserRole)role)))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src =>
                    src.Picture != null ? $"/api/filestorage/{src.Picture.Id}" : string.Empty
                ));

            CreateMap<UserRegisterRequest, UserDetails>()
                .AfterMap((src, dest, context) =>
                {
                    if (context.Items.TryGetValue("PasswordHasher", out var hasherObj) && hasherObj is Func<string, string> hasher)
                    {
                        dest.PasswordHash = hasher(src.Password);
                    }
                    dest.UserRoles = [VibLink.Models.Enumerations.UserRole.USER];
                    dest.Friends = [];
                    dest.BlockedUsers = [];
                });
        }
    }
}