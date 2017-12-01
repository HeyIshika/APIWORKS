using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Password.Models
{
    public class updatepasswordContext : DbContext
    {
        public updatepasswordContext(DbContextOptions<updatepasswordContext> options)
            : base(options)
        {
        }

        public DbSet<UpdatePassword> UserDetails { get; set; }
    }
}