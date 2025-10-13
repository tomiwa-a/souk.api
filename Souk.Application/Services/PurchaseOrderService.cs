using Souk.Application.DTOs;
using Souk.Application.Ports;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;

namespace Souk.Application.Services;

public class PurchaseOrderService(IPurchaseOrdersRepository purchaseOrdersRepository) : IPurchaseOrderService
{
    private readonly IPurchaseOrdersRepository _purchaseOrdersRepository = purchaseOrdersRepository;

    public async Task<PurchaseOrderDto?> GetPurchaseOrderAsync(int id)
    {
        var order = await _purchaseOrdersRepository.GetByIdAsync(id);
        return order == null ? null : MapToDto(order);
    }

    public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersAsync(int page, int pageSize)
    {
        var orders = await _purchaseOrdersRepository.GetAllPurchaseOrdersAsync(page, pageSize);
        return orders.Select(MapToDto);
    }
    public async Task<IEnumerable<PurchaseOrderDto>> GetWarehousePurchaseOrdersAsync(int warehouseId)
    {
        var orders = await _purchaseOrdersRepository.GetWarehouseOrdersAsync(warehouseId);
        return orders.Select(MapToDto);
    }

    private static PurchaseOrderDto MapToDto(PurchaseOrder order)
    {
        return new PurchaseOrderDto
        {
            Id = order.PurchaseOrderId,
            ProductId = order.ProductId,
            SupplierId = order.SupplierId,
            WarehouseId = order.WarehouseId,
            Quantity = order.Quantity,
            OrderedAt = order.OrderedAt,
            ExpectedArrivalDate = order.ExpectedArrivalDate,
            ArrivedAt = order.ArrivedAt
        };
    }
}