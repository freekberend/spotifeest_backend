using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<Trein> treinen { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Party> party { get; set; }
    }
}
