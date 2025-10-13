using Souk.Application.Ports;

namespace Souk.Api.Controllers;

public static class ProductController
{
    public static RouteGroupBuilder RouteProductController(this WebApplication app)
    {
        var group = app.MapGroup("api/products");

        group.MapGet("/{id:int}", async (int id, IProductService productService) =>
        {
            var product = await productService.GetProductAsync(id);
            return product == null ? Results.NotFound() : Results.Ok(product);
        }).WithName("GetProduct");

        group.MapGet("/", async (int? page, int? pageSize, string? name, IProductService productService) =>
        {
            page ??= 1;
            pageSize ??= 10;
            var products = await productService.GetProductsAsync(page.Value, pageSize.Value, name);
            return Results.Ok(products);
            
        });

        group.MapGet("/supplier/{supplierId:int}", async (int supplierId, IProductService productService) =>
        {
            var products = await productService.GetProductsBySupplierAsync(supplierId);
            return Results.Ok(products);
        });
        
        group.MapGet("/warehouse/{warehouseId:int}", async (int warehouseId, IProductService productService) =>
        {
            var products = await productService.GetProductsByWarehouseAsync(warehouseId);
            return Results.Ok(products);
        });

        return group;
    }
}