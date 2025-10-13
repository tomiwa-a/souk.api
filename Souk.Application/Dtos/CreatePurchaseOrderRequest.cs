namespace Souk.Application.DTOs;

public class CreatePurchaseOrderRequest
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
}