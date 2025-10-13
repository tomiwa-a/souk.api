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
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     modelBuilder.ApplyConfiguration(new VendorConfiguration());
    //     modelBuilder.ApplyConfiguration(new PlatformUserConfiguration());
    //     modelBuilder.ApplyConfiguration(new PlatformRoleConfiguration());
    //     modelBuilder.ApplyConfiguration(new PlatformPermissionConfiguration());
    //     // modelBuilder.ApplyConfigurationsFromAssembly(typeof(HostDbContext).Assembly);
    //         
    // }
};