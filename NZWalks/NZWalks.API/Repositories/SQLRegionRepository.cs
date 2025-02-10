using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await GetByIdAsync(id);

            if (region == null)
            {
                return null;
            }

            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

       public async Task<Region?> UpdateAsync(Guid id, Region updates)
        {
            var region = await GetByIdAsync(id);

            if (region == null)
            {
                return null;
            }

            region.Name = updates.Name;
            region.Code = updates.Code;
            region.RegionImageUrl = updates.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return region;
        }
    }
}
