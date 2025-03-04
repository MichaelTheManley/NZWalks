using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Image;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] TODO Add in signup + login + JWT tokens before authorizing
    public class ImagesController : ControllerBase
    {

        private readonly IImageRepository imageRepo;
        private readonly IMapper mapper;
        public ImagesController(IMapper mapper, IImageRepository imageRepo)
        {
            this.mapper = mapper;
            this.imageRepo = imageRepo;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto imageDetails)
        {
            // We first need to validate if the request is correct or not
            ValidateFileUpload(imageDetails);

            if (ModelState.IsValid)
            {
                // Convert Dto to Domain Model
                var imageDomainModel = new Image
                {
                    File = imageDetails.File,
                    FileName = imageDetails.FileName,
                    FileDescription = imageDetails.FileDescription,
                    FileExtension = Path.GetExtension(imageDetails.File.FileName),
                    FileSizeInBytes = imageDetails.File.Length
                    // File Path is added via repo instead
                };

                // User repo to upload image if valid
                await imageRepo.UploadImage(imageDomainModel);

                return Ok(imageDomainModel);
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
