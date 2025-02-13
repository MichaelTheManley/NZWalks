using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walk;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepo;
        private readonly IMapper mapper;
        public WalksController(IMapper mapper, IWalkRepository walkRepo)
        {
            this.mapper = mapper;
            this.walkRepo = walkRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto walk)
        {
            // Map Dto to domain model
            var walkDM = mapper.Map<Walk>(walk);

            // Create walk via interface
            var createdWalk = await walkRepo.CreateWalkAsync(walkDM);

            // Map returned walk back to dto
            var walkDto = mapper.Map<WalkDto>(createdWalk);

            // Return walk dto
            return Ok(walkDto);
        }
    }
}
