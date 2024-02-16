﻿using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Data.Models;

namespace ShoppingListApp.Data
{
    public class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options)
            : base(options)
        {      
            
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductNote> ProductNotes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product() { Id = 1, Name = "Cheese" },
                    new Product() { Id = 2, Name = "Milk" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
