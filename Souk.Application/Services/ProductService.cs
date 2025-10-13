using Souk.Application.DTOs;
using Souk.Application.Ports;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;

namespace Souk.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ProductDto?> GetProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize, string? name)
    {
        var products = await _productRepository.GetAllProductsAsync(page, pageSize, name);
        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsBySupplierAsync(int supplierId)
    {
        var products = await _productRepository.GetProductsBySupplierAsync(supplierId);
        return products.Select(MapToDto);
    }
    public async Task<IEnumerable<ProductDto>> GetProductsByWarehouseAsync(int warehouseId)
    {
        var products = await _productRepository.GetProductsByWarehouseAsync(warehouseId);
        return products.Select(MapToDto);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            ReorderThreshold = product.ReorderThreshold,
            SupplierId = product.SupplierId,
            WarehouseId = product.WarehouseId
        };
    }
}