using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class MockWarehouseService
{
    private readonly List<WarehouseDto> _warehouses = new()
    {
        new WarehouseDto { Id = 1, Name = "Main Warehouse", Location = "NYC", Capacity = 1000 },
        new WarehouseDto { Id = 2, Name = "Secondary Warehouse", Location = "LA", Capacity = 500 }
    };

    public Task<List<WarehouseDto>> GetWarehousesAsync() => Task.FromResult(_warehouses);

    public Task<WarehouseDto?> GetWarehouseAsync(int id) => Task.FromResult(_warehouses.FirstOrDefault(w => w.Id == id));
}