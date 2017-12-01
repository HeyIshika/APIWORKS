using Microsoft.EntityFrameworkCore;
using WebApi.FacebookEntities;

namespace WebApi.Helpers
{
    public class FacebookDataContext : DbContext
    {
        public FacebookDataContext(DbContextOptions<FacebookDataContext> options) : base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}