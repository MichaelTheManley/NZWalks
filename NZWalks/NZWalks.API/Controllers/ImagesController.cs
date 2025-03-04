using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Image;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto imageDetails)
        {
            // We first need to validate if the request is correct or not
            ValidateFileUpload(imageDetails);

            if (ModelState.IsValid)
            {
                // User repo to upload image if valid
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageDetails)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageDetails.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file type. Only .jpg, .jpeg, .png files are allowed.");
            }

            if (imageDetails.File.Length > 10485760) // 10 mb to bytes
            {
                ModelState.AddModelError("File", "Image file is too big. File size more than 10mb.");
            }
        }
    }
}
