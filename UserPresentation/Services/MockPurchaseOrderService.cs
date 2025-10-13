using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class MockPurchaseOrderService
{
    private readonly List<PurchaseOrderDto> _orders = new()
    {
        new PurchaseOrderDto { Id = 1, ProductId = 1, SupplierId = 1, WarehouseId = 1, Quantity = 10, OrderedAt = DateTime.Now, ExpectedArrivalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)) },
        new PurchaseOrderDto { Id = 2, ProductId = 2, SupplierId = 2, WarehouseId = 1, Quantity = 5, OrderedAt = DateTime.Now, ExpectedArrivalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)) }
    };

    public Task<List<PurchaseOrderDto>> GetPurchaseOrdersAsync() => Task.FromResult(_orders);

    public Task<PurchaseOrderDto?> GetPurchaseOrderAsync(int id) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
}