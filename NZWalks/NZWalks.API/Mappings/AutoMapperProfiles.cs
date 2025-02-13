using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.RegionDto;
using NZWalks.API.Models.DTO.Walk;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap(); // We add the reverse map in case we use the reverse somewhere as well
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>();
            CreateMap<Walk, WalkDto>();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
