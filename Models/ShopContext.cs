using Microsoft.EntityFrameworkCore;

namespace APIProject.Models
{
    public class ShopContext : DbContext 
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define how products and categories are related to each other 
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Seed();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
    }
}
