namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation properties
        public Difficulty Difficulty { get; set; } // Non-nullable one to one relationship -- Essentially each walk will have a difficulty, and the difficulty Id will be stored above
        public Region Region { get; set; } // Non-nullable one to one relationship -- The region of the walk will also be stored

    }
}
