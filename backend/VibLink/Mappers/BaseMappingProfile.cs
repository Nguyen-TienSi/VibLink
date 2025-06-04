using AutoMapper;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;

namespace VibLink.Mappers
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            CreateMap<BaseEntity, AuditMetadataResponse>()
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.DeleteAt, opt => opt.MapFrom(src => src.DeletedAt))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
        }
    }
}
