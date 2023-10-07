using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Models
{
    public class EcomDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Define other DbSet properties for your models here

        public EcomDbContext(DbContextOptions<EcomDbContext> context) : base(context)
        {
            
        }
    }
}
