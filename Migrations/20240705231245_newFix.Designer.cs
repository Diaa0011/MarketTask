﻿// <auto-generated />
using Market.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Market.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240705231245_newFix")]
    partial class newFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Market.Model.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("TotalAmount")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Market.Model.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalVat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("productId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Market.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("VAT")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("storeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("storeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Market.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingCost")
                        .HasColumnType("int");

                    b.Property<decimal>("VATPercent")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("Stores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "12 Park Street",
                            Name = "Store 1",
                            ShippingCost = 20,
                            VATPercent = 0.15m
                        },
                        new
                        {
                            Id = 2,
                            Address = "21 Park Street",
                            Name = "Store 12",
                            ShippingCost = 10,
                            VATPercent = 0.25m
                        });
                });

            modelBuilder.Entity("Market.Model.CartItem", b =>
                {
                    b.HasOne("Market.Model.Cart", "cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Market.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cart");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Market.Models.Product", b =>
                {
                    b.HasOne("Market.Models.Store", "store")
                        .WithMany("Products")
                        .HasForeignKey("storeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("store");
                });

            modelBuilder.Entity("Market.Model.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("Market.Models.Store", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}