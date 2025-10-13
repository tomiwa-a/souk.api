using Souk.Domain.Inventory.Entities;

namespace Souk.Domain.Inventory.Aggregates;

public class Warehouse
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public int Capacity { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    
    private readonly List<PurchaseOrder> _purchaseOrders = new();
    public IReadOnlyCollection<PurchaseOrder> PurchaseOrders => _purchaseOrders.AsReadOnly();

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Warehouse() { }

    public static Warehouse Create(string name, string location, int capacity = 0)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(location);
        return new Warehouse
        {
            Name = name,
            Location = location,
            Capacity = capacity,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public void AddProduct(Product product)
    {
        if (!CanAccommodateQuantity(product.Quantity))
        {
            throw new ArgumentException("Product quantity must be less than available warehouse capacity.");
        }
        
        if (_products.All(p => p.Id != product.Id))
        {
            _products.Add(product);
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveProduct(int productId)
    {
        var productToRemove = _products.FirstOrDefault(p => p.Id == productId);
        if (productToRemove != null)
        {
            _products.Remove(productToRemove);
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public int GetCurrentCapacityUsed() => _products.Sum(p => p.Quantity);

    public int GetAvailableCapacity() => Capacity - GetCurrentCapacityUsed();

    public bool CanAccommodateQuantity(int quantity) => GetAvailableCapacity() >= quantity;

    public List<Product> GetProductsNeedingReorder() => _products.Where(p => p.Quantity < p.ReorderThreshold).ToList();

    public void UpdateProductQuantity(Product product, int newQuantity)
    {
        if (!CanAccommodateQuantity(newQuantity))
        {
            throw new ArgumentException("Product quantity must be less than available warehouse capacity.");
        }
        var productToUpdate = _products.FirstOrDefault(p => p.Id == product.Id);
        productToUpdate?.UpdateQuantity(newQuantity);
        UpdatedAt = DateTime.UtcNow;
    }
    
    public PurchaseOrder CreatePurchaseOrder(int productId, int quantity)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            throw new ArgumentException("Product not found in warehouse.");
        }
        if (!CanAccommodateQuantity(quantity))
        {
            throw new ArgumentException("Warehouse does not have enough capacity for this order quantity.");
        }
        var order = PurchaseOrder.Create(productId, product.SupplierId, Id, quantity);
        _purchaseOrders.Add(order);
        UpdatedAt = DateTime.UtcNow;
        return order;
    }

    public void FulfillPurchaseOrder(int orderId)
    {
        var order = _purchaseOrders.FirstOrDefault(o => o.PurchaseOrderId == orderId);
        if (order == null)
        {
            throw new ArgumentException("Purchase order not found.");
        }
        order.FulfilOrder();
        var product = _products.FirstOrDefault(p => p.Id == order.ProductId);
        if (product != null)
        {
            product.UpdateQuantity(product.Quantity + order.Quantity);
        }
        // _purchaseOrders.Remove(order);
        UpdatedAt = DateTime.UtcNow;
    }
}