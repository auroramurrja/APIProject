using Microsoft.EntityFrameworkCore;

namespace APIProject.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name ="Category1" },
                new Category { Id = 2, Name = "Category2" },
                new Category { Id = 3, Name = "Category3" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product11", Sku = "Sku11", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 1 },
                new Product { Id = 2, Name = "Product12", Sku = "Sku12", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 1 },
                new Product { Id = 3, Name = "Product13", Sku = "Sku13", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 1 },
                new Product { Id = 4, Name = "Product14", Sku = "Sku14", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 1 },

                new Product { Id = 5, Name = "Product21", Sku = "Sku21", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 2 },
                new Product { Id = 6, Name = "Product22", Sku = "Sku22", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 2 },
                new Product { Id = 7, Name = "Product23", Sku = "Sku23", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 2 },
                new Product { Id = 8, Name = "Product24", Sku = "Sku24", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 2 },

                new Product { Id = 9, Name = "Product31", Sku = "Sku31", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 3 },
                new Product { Id = 10, Name = "Product32", Sku = "Sku32", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 3 },
                new Product { Id = 11, Name = "Product33", Sku = "Sku33", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 3 },
                new Product { Id = 12, Name = "Product34", Sku = "Sku34", Description = "SomeDesc", IsAvailable = true, Price = 11, CategoryID = 3 }
                );
        }
    }
}
