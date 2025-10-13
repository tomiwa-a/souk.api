using Souk.Application.DTOs;

namespace Souk.Application.Ports;

public interface IProductService
{
    Task<ProductDto?> GetProductAsync(int id);
    Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize, string? name);
    Task<IEnumerable<ProductDto>> GetProductsBySupplierAsync(int supplierId);
    Task<IEnumerable<ProductDto>> GetProductsByWarehouseAsync(int warehouseId);
}