using Souk.Domain.Inventory.Entities;

namespace Souk.Domain.Inventory.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetAllProductsAsync(int page, int pageSize, string? name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetProductsBySupplierAsync(int supplierId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetProductsByWarehouseAsync(int warehouseId, CancellationToken cancellationToken = default);

}