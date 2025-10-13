namespace Souk.Application.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ReorderThreshold { get; set; }
    public int Quantity { get; set; }
    public int SupplierId { get; set; }
    public int WarehouseId { get; set; }
}