using Microsoft.EntityFrameworkCore;
using RainTrackingApi.Models.Domain;

namespace RainTrackingApi.Data
{
    public class RainTrackingDbContext : DbContext
    {
        public RainTrackingDbContext(DbContextOptions<RainTrackingDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<UserRainLog> UserRainLog { get; set; }
        public DbSet<User> User { get; set; }
    }
}
