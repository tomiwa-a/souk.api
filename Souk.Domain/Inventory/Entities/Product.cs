using Souk.Domain.Inventory.Aggregates;

namespace Souk.Domain.Inventory.Entities;

public class Product
{
    public int Id { get; private set; }
    
    public int WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }
    
    public int SupplierId { get; private set; }
    public Supplier Supplier { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int ReorderThreshold { get; private set; }
    public int Quantity { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private Product() { }

    public static Product Create(int supplierId, int warehouseId, string name, string description, int reorderThreshold, int quantity)
    {
        if (reorderThreshold < 0)
        {
            throw new ArgumentException("Reorder threshold must be greater than or equal to zero.");
        }
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be greater than or equal to zero.");
        }
        return new Product
        {
            Name = name,
            Description = description,
            ReorderThreshold = reorderThreshold,
            Quantity = quantity,
            SupplierId = supplierId,
            WarehouseId = warehouseId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateSupplier(int supplierId)
    {
        SupplierId = supplierId;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateQuantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be greater than or equal to zero.");
        }
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}