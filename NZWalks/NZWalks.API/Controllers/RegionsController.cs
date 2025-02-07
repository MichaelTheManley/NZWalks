using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:5001/api/regions 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - domain models
            var regions = dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.Find(id);
            // var region = dbContext.Regions.FirstOrDefault(r => r.Id == id); // Alternative -> We find the first region that has an id that matches the one given

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
        public IActionResult Create([FromBody] AddRegionRequestDto region)
        {
            // Map or convert the DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Use Domain Model to create region and save into database
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updates)
        {
            // Check if region exists
            var regionDomainModel = dbContext.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Dto to domain model
            regionDomainModel.Name = updates.Name;
            regionDomainModel.Code = updates.Code;
            regionDomainModel.RegionImageUrl = updates.RegionImageUrl;

            dbContext.SaveChanges();

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
        [Route("{id: Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            // Attempt to find the region with the given id.
            var regionDomainModel = dbContext.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete region and save changes to database
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

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
