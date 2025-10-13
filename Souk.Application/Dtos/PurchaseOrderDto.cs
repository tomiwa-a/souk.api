namespace Souk.Application.DTOs;

public class PurchaseOrderDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderedAt { get; set; }
    public DateOnly ExpectedArrivalDate { get; set; }
}