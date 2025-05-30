using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record BaseResponseDto
    {
        public required AuditMetadataDto AuditMetadataDto { get; init; }
    }
}
