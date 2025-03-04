using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Difficulty;
using NZWalks.API.Models.DTO.Image;
using NZWalks.API.Models.DTO.Region;
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
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<Image, ImageUploadRequestDto>();
        }
    }
}
