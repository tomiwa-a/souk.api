using Souk.Domain.Inventory.Entities;

namespace Souk.Domain.Inventory.Repositories;

public interface IPurchaseOrdersRepository
{
    Task<PurchaseOrder?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PurchaseOrder>> GetWarehouseOrdersAsync(int warehouseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}