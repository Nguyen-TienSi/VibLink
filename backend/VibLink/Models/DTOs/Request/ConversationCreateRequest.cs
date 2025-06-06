using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Request
{
    public record ConversationCreateRequest
    {
        public string ChatName { get; init; } = string.Empty;

        public IFormFile? ChatPicture { get; init; } = null;

        public ConversationType ConversationType { get; init; }

        public ICollection<string> ParticipantIds { get; init; } = [];
    }
}
