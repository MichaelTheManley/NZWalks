﻿using Microsoft.EntityFrameworkCore;
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
            /*
             * When the dbContext goes to the database to retrieve all the walks, it will also retrieve the associated difficutly and region
             * for a walk. This is done by setting navigation properties in the Walk Domain Model, which are then included in the query below.
             * It finds the associated difficulty and region for each walk by looking at the DifficultyId and RegionId properties in the Walk Domain Model. 
             */
            var walks = await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            // To make it more type safe, we can do the following:
            // var walks = await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();

            // We will opt for the first method as we will have generic repositories later on
            return walks;
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
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
