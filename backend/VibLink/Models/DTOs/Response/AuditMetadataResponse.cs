namespace VibLink.Models.DTOs.Response
{
    public record AuditMetadataResponse
    {
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public DateTime? DeletedAt { get; init; } = null;
        public bool IsDeleted { get; init; } = false;
        public int Version { get; init; } = 0;
    }
}
