using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using WpfApp6.Models;

namespace MyShop.Models.DataAccess;

public partial class ShopContext : DbContext
{
    public ShopContext()
    {
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;user=root;password=941401;database=myShop", ServerVersion.Parse("9.0.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameOfProduct)
                .HasMaxLength(45)
                .HasColumnName("nameOfProduct");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase");

            entity.HasIndex(e => e.IdProduct, "idProduct_idx");

            entity.Property(e => e.Id).HasColumnName("idPurchase");
            entity.Property(e => e.DateOfPurchase).HasColumnName("dateOfPurchase");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.QuantityOfPurchases).HasColumnName("quantityOfPurchases");
            entity.Property(e => e.TotalCost)
                .HasPrecision(10, 2)
                .HasColumnName("totalCost");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("idProduct");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
