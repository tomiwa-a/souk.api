using Souk.Domain.Inventory.Entities;

namespace Souk.Domain.Inventory.Aggregates;

public class Warehouse
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public int Capacity { get; private set; }
    
    public int CapacityUsed { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    
    private readonly List<PurchaseOrder> _purchaseOrders = new();
    public IReadOnlyCollection<PurchaseOrder> PurchaseOrders => _purchaseOrders.AsReadOnly();

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Warehouse() { }

    public static Warehouse Create(string name, string location, int capacity = 0, int capacityUsed = 0)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(location);
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        if (capacityUsed < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacityUsed));
        }
        return new Warehouse
        {
            Name = name,
            Location = location,
            Capacity = capacity,
            CapacityUsed = capacityUsed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public void Update(string name, string location)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(location);
        Name = name;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
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
            CapacityUsed += product.Quantity;
        }
        EnsureCapacityUsedNonNegative();
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveProduct(int productId)
    {
        var productToRemove = _products.FirstOrDefault(p => p.Id == productId);
        if (productToRemove != null)
        {
            _products.Remove(productToRemove);
            CapacityUsed -= productToRemove.Quantity;
        }
        EnsureCapacityUsedNonNegative();
        UpdatedAt = DateTime.UtcNow;
    }

    public int GetCurrentCapacityUsed() => CapacityUsed;

    public int GetAvailableCapacity() => Capacity - GetCurrentCapacityUsed();

    public bool CanAccommodateQuantity(int quantity) => GetAvailableCapacity() >= quantity;

    public List<Product> GetProductsNeedingReorder() => _products.Where(p => p.Quantity < p.ReorderThreshold).ToList();

    public void IncreaseProductQuantity(int productId, int quantity)
    {
        var productToUpdate = _products.FirstOrDefault(p => p.Id == productId);
        if (productToUpdate == null)
        {
            throw new ArgumentException("Product not found in warehouse.");
        }
        int newQuantity = productToUpdate.Quantity + quantity;
        UpdateProductQuantity(productToUpdate, newQuantity);
    }
    public void DecreaseProductQuantity(int productId, int quantity)
    {
        var productToUpdate = _products.FirstOrDefault(p => p.Id == productId);
        if (productToUpdate == null)
        {
            throw new ArgumentException("Product not found in warehouse.");
        }
        int newQuantity = productToUpdate.Quantity - quantity;
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Insufficient quantity.");
        }
        UpdateProductQuantity(productToUpdate, newQuantity);

        if (newQuantity < productToUpdate.ReorderThreshold)
        {
            int targetQuantity = productToUpdate.ReorderThreshold + (productToUpdate.ReorderThreshold / 2);
            int quantityToOrder = Math.Min(GetAvailableCapacity(), Math.Max(0, targetQuantity - newQuantity));
            
            CreatePurchaseOrder(productId, quantityToOrder);
        }
        
    }
    private void UpdateProductQuantity(Product product, int newQuantity)
    {
        int oldQuantity = product.Quantity;
        if (!CanAccommodateQuantity(newQuantity - oldQuantity))
        {
            throw new ArgumentException("Product quantity must be less than available warehouse capacity.");
        }
        product.UpdateQuantity(newQuantity);
        CapacityUsed += (newQuantity - oldQuantity);
        EnsureCapacityUsedNonNegative();
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
            CapacityUsed += order.Quantity;
        }
        EnsureCapacityUsedNonNegative();
        // _purchaseOrders.Remove(order);
        UpdatedAt = DateTime.UtcNow;
    }
    
    private void EnsureCapacityUsedNonNegative()
    {
        if (CapacityUsed < 0) throw new InvalidOperationException("CapacityUsed cannot be negative.");
    }
}