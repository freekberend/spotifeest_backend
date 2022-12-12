using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Party> parties { get; set; }
        public DbSet<PartyPreference> partypreferences { get; set; }
        public DbSet<Preference> preferences { get; set; }
        public DbSet<Song> songs { get; set; }

    }
}
