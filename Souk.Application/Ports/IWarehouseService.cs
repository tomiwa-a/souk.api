using Souk.Application.DTOs;
using Souk.Domain.Inventory.Aggregates;

namespace Souk.Application.Ports;

public interface IWarehouseService
{
    Task<WarehouseDto?> GetWarehouseAsync(int id);
    Task<Warehouse?> GetWarehouseWithProductsAsync(int id);
    Task<IEnumerable<WarehouseDto>> GetWarehousesAsync(int page, int pageSize, string? name);
    Task<WarehouseDto> CreateWarehouseAsync(string name, string location, int capacity);
    Task<WarehouseDto> UpdateWarehouseAsync(int id, string name, string location);
    Task<WarehouseDto> AddProductToWarehouseAsync(CreateProductRequest request);
    Task<WarehouseDto> IncreaseProductQuantityAsync(int warehouseId, int productId, int quantity);
    Task<WarehouseDto> DecreaseProductQuantityAsync(int warehouseId, int productId, int quantity);
    Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request);
    Task FulfillPurchaseOrderAsync(int orderId);
}