using System;
using Microsoft.EntityFrameworkCore;
using Outbox.API.Models;

namespace Outbox.API.Context;

public sealed class OutboxContext : DbContext
{
    public OutboxContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderOutbox>  OrderOutbox { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(opt =>
        {
            opt.HasKey(x => x.Id);
            opt.HasIndex(x => new { x.CustomerEmail, x.Id });

            opt.Property(x => x.ProductName).HasColumnType("nvarchar(50)");
            opt.Property(x => x.CustomerEmail).IsRequired().HasColumnType("nvarchar(50)");

        });
        modelBuilder.Entity<OrderOutbox>(opt =>
        {
            opt.HasKey(x => x.Id);
        });

    }
}
