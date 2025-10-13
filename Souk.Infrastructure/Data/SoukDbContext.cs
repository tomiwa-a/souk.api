using Microsoft.EntityFrameworkCore;
using Souk.Domain.Inventory.Aggregates;
using Souk.Domain.Inventory.Entities;

namespace Souk.Infrastructure.Data;



public class SoukDbContext(DbContextOptions<SoukDbContext> options) 
    : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //
        // modelBuilder.Entity<Warehouse>()
        //     .HasMany(w => w.Products)
        //     .WithOne()
        //     .HasForeignKey(p => p.WarehouseId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // modelBuilder.Entity<Warehouse>()
        //     .HasMany(w => w.PurchaseOrders)
        //     .WithOne()
        //     .HasForeignKey(po => po.WarehouseId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // modelBuilder.Entity<Product>()
        //     .HasOne<Supplier>()
        //     .WithMany()
        //     .HasForeignKey(p => p.SupplierId);
        //
        // modelBuilder.Entity<PurchaseOrder>()
        //     .HasOne<Product>()
        //     .WithMany()
        //     .HasForeignKey(po => po.ProductId);
    }
};