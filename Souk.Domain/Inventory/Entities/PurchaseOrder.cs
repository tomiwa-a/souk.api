using Souk.Domain.Inventory.Aggregates;

namespace Souk.Domain.Inventory.Entities;

public class PurchaseOrders
{
    public int OrderId { get; private set; }
    
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    
    public int SupplierId { get; private set; }
    public Supplier Supplier { get; private set; }
    
    public int WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }
    
    public int Quantity { get; private set; }
    
    public DateTime OrderedAt { get; private set; }
    public DateOnly ExpectedArrivalDate => DateOnly.FromDateTime(OrderedAt).AddDays(3);

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private PurchaseOrders() { }

    public static PurchaseOrders Create(int productId, int supplierId, int warehouseId,  int quantity)
    {
        if (productId <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero");
        }
        if (supplierId <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero");
        }
        if (warehouseId <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }
        
        return new PurchaseOrders
        {
            ProductId = productId,
            SupplierId = supplierId,
            WarehouseId = warehouseId,
            Quantity = quantity,
            OrderedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}