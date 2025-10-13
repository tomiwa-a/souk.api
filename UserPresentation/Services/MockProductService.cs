using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class MockProductService
{
    private readonly List<ProductDto> _products = new()
    {
        new ProductDto { Id = 1, Name = "Laptop", Description = "Gaming Laptop", Quantity = 5, ReorderThreshold = 10, SupplierId = 1, WarehouseId = 1 },
        new ProductDto { Id = 2, Name = "Mouse", Description = "Wireless Mouse", Quantity = 20, ReorderThreshold = 15, SupplierId = 2, WarehouseId = 1 }
    };

    public Task<List<ProductDto>> GetProductsAsync() => Task.FromResult(_products);

    public Task<ProductDto?> GetProductAsync(int id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<List<ProductDto>> GetProductsBySupplierAsync(int supplierId) => Task.FromResult(_products.Where(p => p.SupplierId == supplierId).ToList());
}