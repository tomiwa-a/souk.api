using Microsoft.EntityFrameworkCore;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;
using Souk.Infrastructure.Data;

namespace Souk.Infrastructure.Data.Repository;

public class PurchaseOrdersRepository(SoukDbContext context) : IPurchaseOrdersRepository
{
    private readonly SoukDbContext _context = context;

    public async Task<PurchaseOrder?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.PurchaseOrders.FindAsync([id], cancellationToken);
    }
    
    public async Task<IEnumerable<PurchaseOrder>> GetWarehouseOrdersAsync(int warehouseId, CancellationToken cancellationToken = default)
    {
        return await _context.PurchaseOrders.Where(p => p.WarehouseId == warehouseId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _context.PurchaseOrders.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}