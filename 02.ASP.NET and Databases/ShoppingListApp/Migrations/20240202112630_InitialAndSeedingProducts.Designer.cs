﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingListApp.Data;

#nullable disable

namespace ShoppingListApp.Migrations
{
    [DbContext(typeof(ShoppingListDbContext))]
    [Migration("20240202112630_InitialAndSeedingProducts")]
    partial class InitialAndSeedingProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShoppingListApp.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Product Name");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasComment("Shopping List Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cheese"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Milk"
                        });
                });

            modelBuilder.Entity("ShoppingListApp.Data.Models.ProductNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ProductNote Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("ProductNote Content");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductNotes");

                    b.HasComment("Shopping List ProductNote");
                });

            modelBuilder.Entity("ShoppingListApp.Data.Models.ProductNote", b =>
                {
                    b.HasOne("ShoppingListApp.Data.Models.Product", "Product")
                        .WithMany("ProductNotes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShoppingListApp.Data.Models.Product", b =>
                {
                    b.Navigation("ProductNotes");
                });
#pragma warning restore 612, 618
        }
    }
}