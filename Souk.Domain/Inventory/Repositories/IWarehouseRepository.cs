using Souk.Domain.Inventory.Aggregates;

namespace Souk.Domain.Inventory.Repositories;

public interface IWarehouseRepository
{
    Task<Warehouse> AddAsync(Warehouse warehouse);
    Task<Warehouse> UpdateAsync(Warehouse warehouse);
    Task<Warehouse?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Warehouse?> GetWarehouseWithProductsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Warehouse>> GetAllWarehousesAsync(int page, int pageSize, string? name, CancellationToken cancellationToken = default);
}