using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
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

        // GET All Walks
        // GET: /api/walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortingBy, [FromQuery] bool? isAscending)
        {
            // Get all walks via interface
            var walks = await walkRepo.GetAllWalksAsync(filterOn, filterQuery, sortingBy, isAscending);

            // Convert walks to list of dto's
            var walkDtos = mapper.Map<List<WalkDto>>(walks);

            // Return Dto's
            return Ok(walkDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            // Get walk via interface
            var walk = await walkRepo.GetWalkByIdAsync(id);

            if (walk == null)
            {
                return NotFound(walk);
            }

            // Convert walk to dto
            var walkDto = mapper.Map<WalkDto>(walk);

            // Return Dto's
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromBody] UpdateWalkRequestDto updates, [FromRoute] Guid id)
        {
            // Convert dto to domain model
            var walkDM = mapper.Map<Walk>(updates);

            // Update walk via interface
            var updatedWalk = await walkRepo.UpdateWalkAsync(id, walkDM);

            if (updatedWalk == null)
            {
                return NotFound(updatedWalk);
            }

            // Convert walk to dto
            var walkDto = mapper.Map<WalkDto>(updatedWalk);

            // Return Dto's
            return Ok(walkDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto walk)
        {
             // Map Dto to domain model
            var walkDM = mapper.Map<Walk>(walk);

            // Create walk via interface
            var createdWalk = await walkRepo.CreateWalkAsync(walkDM);

            // Map returned walk back to dto
            var walkDto = mapper.Map<WalkDto>(createdWalk);

            // Return walk dto
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            // Delete walk via interface
            var deletedWalk = await walkRepo.DeleteWalkAsync(id);

            if (deletedWalk == null)
            {
                return NotFound(deletedWalk);
            }

            // Convert walk to dto
            var walkDto = mapper.Map<WalkDto>(deletedWalk);

            // Return walk dto
            return Ok(walkDto);
        }
    }
}
