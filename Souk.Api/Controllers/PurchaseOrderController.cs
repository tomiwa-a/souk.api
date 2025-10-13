using Souk.Application.Ports;

namespace Souk.Api.Controllers;

public static class PurchaseOrderController
{
    public static RouteGroupBuilder RoutePurchaseOrderController(this WebApplication app)
    {
        var group = app.MapGroup("api/purchase-orders");

        group.MapGet("/{id:int}", async (int id, IPurchaseOrderService purchaseOrderService) =>
        {
            var order = await purchaseOrderService.GetPurchaseOrderAsync(id);
            return order == null ? Results.NotFound() : Results.Ok(order);
        });

        group.MapGet("/", async (int? page, int? pageSize, IPurchaseOrderService purchaseOrderService) =>
        {
            page ??= 1;
            pageSize ??= 10;
            var orders = await purchaseOrderService.GetPurchaseOrdersAsync(page.Value, pageSize.Value);
            return Results.Ok(orders);
        });

        return group;
    }
}