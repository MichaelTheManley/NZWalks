﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:5001/api/regions 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepo;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepo)
        {
            this.dbContext = dbContext;
            this.regionRepo = regionRepo;
        }

        // GET ALL REGIONS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - domain models
            var regions = await regionRepo.GetAllAsync();

            // Map domain models to DTOs
            var regionsDto = new List<RegionDto>();

            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGION (BY ID)
        [HttpGet]
        [Route("{id:Guid}")] // Typesafe
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var region = await dbContext.Regions.FindAsync(id); // This method gives errors when working with Guid and async. programming
            // var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id); // Alternative -> We find the first region that has an id that matches the one given
            var region = await regionRepo.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound(region);
            }
            else
            {
                var regionDto = new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };

                return Ok(regionDto);
            }
        }

        // POST: https://localhost:5001/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto region)
        {
            // Map or convert the DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Create the region in the database using the interface, and store the resulting region in a variable to get the ID.
            regionDomainModel = await regionRepo.CreateAsync(regionDomainModel);

            // Map Domain Model back to DTO t return to client
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);  /* Posts return a 201, and CreatedAtAction returns the 201
                                                                                             * We also check to see if the newly created region is in 
                                                                                             * the database by using the GetById method.
                                                                                             */
        }

        // UPDATE: https://localhost:5001/api/regions
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updates)
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region()
            {
                Name = updates.Name,
                Code = updates.Code,
                RegionImageUrl = updates.RegionImageUrl
            };
            // Via the interface, we attempt to update the region with the given id and updates
            regionDomainModel = await regionRepo.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound(regionDomainModel);
            }

            // Convert Domain Model back to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // Return DTO back to client
            return Ok(regionDto);
        }

        // DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Attempt to find the region with the given id.
            var regionDomainModel = await regionRepo.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert domain model back to Dto and return to client
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // Return Dto back to client
            return Ok(regionDto); // You can also send the empty okay resposne back
        }
    }
}
