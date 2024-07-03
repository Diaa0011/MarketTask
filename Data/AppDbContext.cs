using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Product>()
                        .Property(p => p.VAT)
                        .HasColumnType("decimal(18, 2)");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

    }
}