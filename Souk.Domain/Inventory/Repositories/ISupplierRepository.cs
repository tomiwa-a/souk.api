using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.ValueObjects;

namespace Souk.Domain.Inventory.Repositories;

public interface ISupplierRepository
{
    Task<Supplier> AddAsync(Supplier supplier);
    Task<Supplier> UpdateAsync(Supplier supplier);
    Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<Supplier?> GetByEmailAsync(string emailAddress, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Supplier>> GetAllSuppliersAsync(int page, int pageSize, string? name, CancellationToken cancellationToken = default);
}