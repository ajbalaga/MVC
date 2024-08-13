using Microsoft.EntityFrameworkCore;
using SimpleWebApp.data.Model;

namespace SimpleWebApp.core.Context
{
    public partial class SimpleDbContext : DbContext
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
            : base(options)
        {
        }   
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
