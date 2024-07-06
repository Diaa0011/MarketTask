using Market.Model;
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
            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.VATPercent)
                    .HasColumnType("decimal(5,2)"); // Define precision and scale
            });

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

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)"); // Adjust the precision and scale as needed

                entity.Property(e => e.TotalVat)
                    .HasColumnType("decimal(18,2)"); // Adjust the precision and scale as needed
                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.CartItemId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.cart)
                .WithMany(c => c.CartItems);


            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.product);


            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartId);

            modelBuilder.Entity<Cart>()
                        .HasMany(c => c.CartItems)
                        .WithOne(c => c.cart);
            modelBuilder.Entity<Cart>()
                        .Property(c => c.TotalAmount)
                        .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = 1,
                    Name = "Store 1",
                    Address = "12 Park Street",
                    VATPercent = 0.15m,
                    ShippingCost = 20

                },
                new Store
                {
                    Id = 2,
                    Name = "Store 12",
                    Address = "21 Park Street",
                    VATPercent = 0.25m,
                    ShippingCost = 10
                }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}