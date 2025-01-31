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

        // When you run entity framework core migrations, these 3 properties will create tables inside the new database.
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
