using Microsoft.EntityFrameworkCore;
using Souk.Domain.Inventory.Aggregates;
using Souk.Domain.Inventory.Repositories;
using Souk.Infrastructure.Data;

namespace Souk.Infrastructure.Data.Repository;

public class WarehouseRepository(SoukDbContext context) : IWarehouseRepository
{
    private readonly SoukDbContext _context = context;

    public async Task<Warehouse> AddAsync(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }

    public async Task<Warehouse> UpdateAsync(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }

    public async Task<Warehouse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Warehouses.FindAsync([id], cancellationToken);
    }

    public async Task<Warehouse?> GetWarehouseWithProductsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Warehouses.Include(w => w.Products).FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync(int page, int pageSize, string? name, CancellationToken cancellationToken = default)
    {
        var query = _context.Warehouses.AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(w => w.Name.Contains(name));
        }
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}