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
            modelBuilder.Entity<Store>()
                        .HasMany(s => s.Products)
                        .WithOne(p => p.store);
            modelBuilder.Entity<Product>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Product>()
                        .Property(p => p.VAT)
                        .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.store)
                        .WithMany(s => s.Products)
                        .IsRequired();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = 1,
                    Name = "Store 1",
                    Address = "12 Park Street"
                },
                new Store
                {
                    Id = 2,
                    Name = "Store 12",
                    Address = "21 Park Street"
                }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

    }
}