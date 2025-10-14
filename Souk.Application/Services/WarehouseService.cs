using Souk.Application.DTOs;
using Souk.Application.Ports;
using Souk.Domain.Inventory.Aggregates;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;

namespace Souk.Application.Services;

public class WarehouseService(IWarehouseRepository warehouseRepository, IProductRepository productRepository, IPurchaseOrdersRepository purchaseOrdersRepository, ISupplierRepository supplierRepository) : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IPurchaseOrdersRepository _purchaseOrdersRepository = purchaseOrdersRepository;
    private readonly ISupplierRepository _supplierRepository = supplierRepository;

    public async Task<WarehouseDto?> GetWarehouseAsync(int id)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(id);
        return warehouse == null ? null : MapToDto(warehouse);
    }

    public async Task<Warehouse?> GetWarehouseWithProductsAsync(int id)
    {
        var warehouse = await _warehouseRepository.GetWarehouseWithProductsAsync(id);
        return warehouse;
    }

    public async Task<IEnumerable<WarehouseDto>> GetWarehousesAsync(int page, int pageSize, string? name)
    {
        var warehouses = await _warehouseRepository.GetAllWarehousesAsync(page, pageSize, name);
        return warehouses.Select(MapToDto);
    }

    public async Task<WarehouseDto> CreateWarehouseAsync(string name, string location, int capacity)
    {
        var warehouse = Warehouse.Create(name, location, capacity);
        var added = await _warehouseRepository.AddAsync(warehouse);
        return MapToDto(added);
    }

    public async Task<WarehouseDto> UpdateWarehouseAsync(int id, string name, string location)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(id);
        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not found.");
        }
        warehouse.Update(name, location);
        var updated = await _warehouseRepository.UpdateAsync(warehouse);
        return MapToDto(updated);
    }

    public async Task<WarehouseDto> AddProductToWarehouseAsync(CreateProductRequest request)
    {
        var warehouse = await _warehouseRepository.GetWarehouseWithProductsAsync(request.WarehouseId);
        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not found.");
        }
        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId);
        if (supplier == null)
        {
            throw new ArgumentException("Supplier not found.");
        }
        var product = Product.Create(request.SupplierId, request.WarehouseId, request.Name, request.Description, request.ReorderThreshold, request.Quantity);
        warehouse.AddProduct(product);
        var updated = await _warehouseRepository.UpdateAsync(warehouse);
        return MapToDto(updated);
    }

    public async Task<WarehouseDto> IncreaseProductQuantityAsync(int warehouseId, int productId, int quantity)
    {
        var warehouse = await _warehouseRepository.GetWarehouseWithProductsAsync(warehouseId);
        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not found.");
        }
        warehouse.IncreaseProductQuantity(productId, quantity);
        var updated = await _warehouseRepository.UpdateAsync(warehouse);
        return MapToDto(updated);
    }
    
    public async Task<WarehouseDto> DecreaseProductQuantityAsync(int warehouseId, int productId, int quantity)
    {
        var warehouse = await _warehouseRepository.GetWarehouseWithProductsAsync(warehouseId);
        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not found.");
        }
        warehouse.DecreaseProductQuantity(productId, quantity);
        var updated = await _warehouseRepository.UpdateAsync(warehouse);
        return MapToDto(updated);
    }
    
    

    public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request)
    {
        var warehouse = await _warehouseRepository.GetWarehouseWithProductsAsync(request.WarehouseId);
        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not found.");
        }
        var order = warehouse.CreatePurchaseOrder(request.ProductId, request.Quantity);
        var updated = await _warehouseRepository.UpdateAsync(warehouse);
        return new PurchaseOrderDto
        {
            Id = order.PurchaseOrderId,
            ProductId = order.ProductId,
            SupplierId = order.SupplierId,
            WarehouseId = order.WarehouseId,
            Quantity = order.Quantity,
            OrderedAt = order.OrderedAt,
            ExpectedArrivalDate = order.ExpectedArrivalDate
        };
    }

    public async Task FulfillPurchaseOrderAsync(int orderId)
    {
        var order = await _purchaseOrdersRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException("Purchase order not found.");
        }
        var warehouse = await _warehouseRepository.GetWarehouseWithProductsAsync(order.WarehouseId);
        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not found.");
        }
        warehouse.FulfillPurchaseOrder(orderId);
        await _warehouseRepository.UpdateAsync(warehouse);
    }

    private static WarehouseDto MapToDto(Warehouse warehouse)
    {
        return new WarehouseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity
        };
    }
}