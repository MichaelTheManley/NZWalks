using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortingBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 5); // Defaulted to null
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk updates);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
