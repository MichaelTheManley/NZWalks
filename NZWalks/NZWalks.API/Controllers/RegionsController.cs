using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
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
                return Ok(region);
            }
        }
    }
}
