using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopeeFoodChap2.Models;

namespace ShopeeFoodChap2.Data;

public partial class ShopeeFoodChap2Context : DbContext
{
    public ShopeeFoodChap2Context()
    {
    }

    public ShopeeFoodChap2Context(DbContextOptions<ShopeeFoodChap2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3214EC07D5349247");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.CurrentOrder).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CurrentOrderId)
                .HasConstraintName("FK__Drivers__Current__286302EC");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07B4EAFDF0");

            entity.Property(e => e.DeliveryAddress).HasMaxLength(200);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).HasMaxLength(50);

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__Orders__Restaura__29572725");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC0724600632");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
