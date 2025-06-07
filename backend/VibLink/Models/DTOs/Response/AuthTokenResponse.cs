namespace VibLink.Models.DTOs.Response
{
    public record AuthTokenResponse
    {
        public string AccessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
    }
}
