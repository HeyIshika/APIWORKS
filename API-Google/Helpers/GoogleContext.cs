using Microsoft.EntityFrameworkCore;
using WebApi.GoogleEntities;

namespace WebApi.Helpers
{
    public class GoogleDataContext : DbContext
    {
        public GoogleDataContext(DbContextOptions<GoogleDataContext> options) : base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}