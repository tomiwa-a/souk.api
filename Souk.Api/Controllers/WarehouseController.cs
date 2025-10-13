using Souk.Application.DTOs;
using Souk.Application.Ports;

namespace Souk.Api.Controllers;

public static class WarehouseController
{
    public static RouteGroupBuilder RouteWarehouseController(this WebApplication app)
    {
        var group = app.MapGroup("api/warehouses");

        group.MapGet("/{id:int}", async (int id, IWarehouseService warehouseService) =>
        {
            var warehouse = await warehouseService.GetWarehouseAsync(id);
            return warehouse == null ? Results.NotFound() : Results.Ok(warehouse);
        });

        group.MapGet("/{id:int}/products", async (int id, IWarehouseService warehouseService) =>
        {
            var warehouse = await warehouseService.GetWarehouseWithProductsAsync(id);
            return warehouse == null ? Results.NotFound() : Results.Ok(warehouse);
        });

        group.MapGet("/", async (int? page, int? pageSize, string? name, IWarehouseService warehouseService) =>
        {
            page ??= 1;
            pageSize ??= 10;
            var warehouses = await warehouseService.GetWarehousesAsync(page.Value, pageSize.Value, name);
            return Results.Ok(warehouses);
        });

        group.MapPost("/", async (string name, string location, int capacity, IWarehouseService warehouseService) =>
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location) || capacity <= 0)
            {
                return Results.BadRequest("Name, location, and positive capacity are required.");
            }
            var warehouse = await warehouseService.CreateWarehouseAsync(name, location, capacity);
            return Results.Created($"/api/warehouses/{warehouse.Id}", warehouse);
        });

        group.MapPut("/{id:int}", async (int id, string name, string location, IWarehouseService warehouseService) =>
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
            {
                return Results.BadRequest("Name and location are required.");
            }
            try
            {
                var warehouse = await warehouseService.UpdateWarehouseAsync(id, name, location);
                return Results.Ok(warehouse);
            }
            catch (ArgumentException)
            {
                return Results.NotFound();
            }
        });

        group.MapPost("/{id:int}/products", async (int id, CreateProductRequest request, IWarehouseService warehouseService) =>
        {
            try
            {
                var warehouse = await warehouseService.AddProductToWarehouseAsync(request);
                return Results.Created($"/api/warehouses/{warehouse.Id}/products", warehouse);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id:int}/products/{productId:int}/increase", async (int id, int productId, int quantity, IWarehouseService warehouseService) =>
        {
            if (quantity < 0)
            {
                return Results.BadRequest("Quantity must be non-negative.");
            }
            try
            {
                var warehouse = await warehouseService.IncreaseProductQuantityAsync(id, productId, quantity);
                return Results.Ok(warehouse);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        
        group.MapPut("/{id:int}/products/{productId:int}/decrease", async (int id, int productId, int quantity, IWarehouseService warehouseService) =>
        {
            if (quantity < 0)
            {
                return Results.BadRequest("Quantity must be non-negative.");
            }
            try
            {
                var warehouse = await warehouseService.DecreaseProductQuantityAsync(id, productId, quantity);
                return Results.Ok(warehouse);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPost("/{id:int}/purchase-orders", async (int id, CreatePurchaseOrderRequest request, IWarehouseService warehouseService) =>
        {
            try
            {
                var order = await warehouseService.CreatePurchaseOrderAsync(request);
                return Results.Created($"/api/warehouses/{id}/purchase-orders/{order.Id}", order);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPost("/{warehouseId:int}/purchase-orders/{orderId:int}/fulfill", async (int warehouseId, int orderId, IWarehouseService warehouseService) =>
        {
            try
            {
                await warehouseService.FulfillPurchaseOrderAsync(orderId);
                return Results.Ok();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return group;
    }
}