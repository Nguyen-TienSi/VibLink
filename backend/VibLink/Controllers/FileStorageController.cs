using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VibLink.Services.Internal;

namespace VibLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;

        public FileStorageController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpGet("picture/{id}")]
        public async Task<IActionResult> GetPicture(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }
            var picture = await _fileStorageService.GetPictureAsync(objectId);
            if (picture == null || picture.FileData == null)
            {
                return NotFound("Picture not found.");
            }
            return File(picture.FileData, picture.ContentType ?? "image/*");
        }
    }
}
