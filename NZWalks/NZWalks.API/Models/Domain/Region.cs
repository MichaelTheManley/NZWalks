namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        // Non-nullable properties
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        // Nullable property
        public string? RegionImageUrl { get; set; } // Question mark signifies nullable property
    }
}
