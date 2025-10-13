using Microsoft.EntityFrameworkCore;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;
using Souk.Infrastructure.Data;

namespace Souk.Infrastructure.Data.Repository;

public class ProductRepository(SoukDbContext context) : IProductRepository
{
    private readonly SoukDbContext _context = context;

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(int page, int pageSize, string? name, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplierAsync(int supplierId, CancellationToken cancellationToken = default)
    {
        return await _context.Products.Where(p => p.SupplierId == supplierId).ToListAsync(cancellationToken);
    }
}