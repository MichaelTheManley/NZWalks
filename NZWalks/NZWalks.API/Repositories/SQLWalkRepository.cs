using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            var walks = await dbContext.Walks.ToListAsync();
            return walks;
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            return walk;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk updates)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (walk == null)
            {
                return null;
            }

            walk.Name = updates.Name;
            walk.Description = updates.Description;
            walk.LengthInKm = updates.LengthInKm;
            walk.WalkImageUrl = updates.WalkImageUrl;
            walk.DifficultyId = updates.DifficultyId;
            walk.RegionId = updates.RegionId;
            walk.Difficulty = await dbContext.Difficulties.FirstOrDefaultAsync(d => d.Id == updates.DifficultyId);
            walk.Region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == updates.RegionId);

            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }        
    }
}
