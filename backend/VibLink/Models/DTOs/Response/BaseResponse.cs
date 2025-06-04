namespace VibLink.Models.DTOs.Response
{
    public record BaseResponse
    {
        public required AuditMetadataResponse AuditMetadataResponse { get; init; }
    }
}
