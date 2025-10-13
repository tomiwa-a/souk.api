using Souk.Application.DTOs;

namespace Souk.Application.Ports;

public interface IPurchaseOrderService
{
    Task<PurchaseOrderDto?> GetPurchaseOrderAsync(int id);
    Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersAsync(int page, int pageSize);
    Task<IEnumerable<PurchaseOrderDto>> GetWarehousePurchaseOrdersAsync(int warehouseId);
}