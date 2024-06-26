using BlogUNAH.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API.Database
{
    public class BlogUNAHContext : DbContext
    {
        public BlogUNAHContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CategoryEntity> categories { get; set; }
    }
}
