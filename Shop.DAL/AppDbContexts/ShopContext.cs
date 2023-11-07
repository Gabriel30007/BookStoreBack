using Microsoft.EntityFrameworkCore;
using Shop.DAL.Entities;
using System;

namespace Shop.DAL.AppDbContexts;

public class ShopContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Bucket> Bucket { get; set; }
    public DbSet<Author> Author { get; set; }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bucket>()
            .HasOne<Product>()
            .WithMany(e => e.Buckets)
            .HasForeignKey(e=>e.productID)
            .IsRequired(false);
        modelBuilder.Entity<Bucket>()
            .HasOne<User>()
            .WithMany(e => e.Buckets)
            .HasForeignKey(e => e.userID)
            .IsRequired(false);
        modelBuilder.Entity<Product>().HasOne<Author>()
            .WithMany(e => e.Products)
            .HasForeignKey(e=>e.authorID);
    }
}
