using System.ComponentModel.DataAnnotations;

namespace VibLink.Models.DTOs.Request
{
    public record UserRegisterRequest
    {
        [Required]
        public string Email { get; init; } = string.Empty;

        [Required]
        public string Password { get; init; } = string.Empty;

        [Required]
        public string FirstName { get; init; } = string.Empty;

        [Required]
        public string LastName { get; init; } = string.Empty;

        public IFormFile? Picture { get; init; } = null;
    }
}
