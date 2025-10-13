using Microsoft.EntityFrameworkCore;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;
using Souk.Domain.Inventory.ValueObjects;
using Souk.Infrastructure.Data;

namespace Souk.Infrastructure.Data.Repository;

public class SupplierRepository(SoukDbContext context) : ISupplierRepository
{
    private readonly SoukDbContext _context = context;

    public async Task<Supplier> AddAsync(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier> UpdateAsync(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Suppliers.FindAsync([id], cancellationToken);
    }
    public async Task<Supplier?> GetByEmailAsync(string emailAddress, CancellationToken cancellationToken = default)
    {
        return await _context.Suppliers.Where(x => x.EmailAddress == emailAddress).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync(int page, int pageSize, string? name, CancellationToken cancellationToken = default)
    {
        var query = _context.Suppliers.AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name.Contains(name));
        }
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}