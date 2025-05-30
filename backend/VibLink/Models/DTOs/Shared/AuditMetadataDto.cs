namespace VibLink.Models.DTOs.Shared
{
    public record AuditMetadataDto
    {
        public DateTime CreateAt { get; init; }
        public DateTime UpdateAt { get; init; }
        public DateTime? DeleteAt { get; init; } = null;
        public bool IsDeleted { get; init; } = false;
        public int Version { get; init; } = 0;
    }
}
