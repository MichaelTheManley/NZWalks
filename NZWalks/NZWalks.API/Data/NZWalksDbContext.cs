using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

// This class is the bridge between the controller and the database. This class represents a session with
// the database, and provides a set of API's that allow us to perform database operations. This class is
// also responsible for maintaining a connection to the database and tracking changes to data. It also
// provides a way to define the database schema using entity/domain classes; these then map directly
// to database tables. 
namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) // We then pass the dbContextOptions to the Base class.
        {
            
        }

        // When you run entity framework core migrations, these 4 properties will create tables inside the new database.
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        // Seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder) // This model builder is used as part of the data seeding process.
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("478172f7-0576-4abd-ac66-32f73c3058d7"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8e59e18c-1c88-4dca-bfd4-77933315a677"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("97ee2524-e280-4e7b-803a-28b56820a2d8"),
                    Name = "Hard"
                }
            };

            // We use the modelBuilder to seed the data, with the type of entity we want to seed given in <> (in this case, Difficulty).
            modelBuilder.Entity<Difficulty>().HasData(difficulties); // .HasData() provides data for the given list.

            // Seed data for regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("7b5df87d-0264-4ddf-9cc3-30e88fdb380d"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "auck.image.co.za"
                },
                new Region()
                {
                    Id = Guid.Parse("9c4f96f2-ae51-43f4-a63a-b516026b1068"),
                    Name = "South Africa",
                    Code = "RSA",
                    RegionImageUrl = "rsa.image.co.za"
                },
                new Region()
                {
                    Id = Guid.Parse("3f840971-4f12-47cd-9dbd-e01c003f5e2b"),
                    Name = "Australia",
                    Code = "AUS",
                    RegionImageUrl = "aus.image.co.za"
                },
                new Region()
                {
                    Id = Guid.Parse("01c9f4e7-8fde-4c3c-8a8a-bf6adadde383"),
                    Name = "England",
                    Code = "UK",
                    RegionImageUrl = "london.image.co.za"
                },
                new Region()
                {
                    Id = Guid.Parse("3ed29439-e89f-428c-a650-45669c12d200"),
                    Name = "New York",
                    Code = "NY",
                    RegionImageUrl = "nyork.image.co.za"
                }
            };

            // Seed the regions data
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
