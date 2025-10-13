using Souk.Domain.Inventory.Aggregates;
using Souk.Domain.Inventory.Enum;

namespace Souk.Domain.Inventory.Entities;

public class PurchaseOrder
{
    public int PurchaseOrderId { get; private set; }
    
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    
    public int SupplierId { get; private set; }
    public Supplier Supplier { get; private set; }
    
    public int WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }
    
    public int Quantity { get; private set; }
    
    public DateTime OrderedAt { get; private set; }
    public DateTime? ArrivedAt { get; private set; }
    
    public PurchaseStatus Status { get; private set; }
    public DateOnly ExpectedArrivalDate => DateOnly.FromDateTime(OrderedAt).AddDays(3);

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private PurchaseOrder() { }

    public static PurchaseOrder Create(int productId, int supplierId, int warehouseId,  int quantity)
    {
        if (productId <= 0 || supplierId <= 0 || warehouseId <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }
        
        return new PurchaseOrder
        {
            ProductId = productId,
            SupplierId = supplierId,
            WarehouseId = warehouseId,
            Quantity = quantity,
            Status = PurchaseStatus.Pending,
            OrderedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void FulfilOrder()
    {
        Status = PurchaseStatus.Fulfilled;
        ArrivedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}