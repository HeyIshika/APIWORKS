using Microsoft.EntityFrameworkCore;
using WebApi.TokenDtos;


namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TokenDto> TokenDto { get; set; }
        
    }
}