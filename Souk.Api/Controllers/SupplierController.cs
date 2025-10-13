using Souk.Application.Ports;

namespace Souk.Api.Controllers;

public static class SupplierController
{
    public static RouteGroupBuilder RouteSupplierController(this WebApplication app)
    {
        var group = app.MapGroup("api/suppliers");

        group.MapGet("/{id:int}", async (int id, ISupplierService supplierService) =>
        {
            var supplier = await supplierService.GetSupplierAsync(id);
            return supplier == null ? Results.NotFound() : Results.Ok(supplier);
        });

        group.MapGet("/", async (int? page, int? pageSize, string? name, ISupplierService supplierService) =>
        {
            page ??= 1;
            pageSize ??= 10;
            var suppliers = await supplierService.GetSuppliersAsync(page.Value, pageSize.Value, name);
            return Results.Ok(suppliers);
        });

        group.MapPost("/", async (string name, string email, ISupplierService supplierService) =>
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                return Results.BadRequest("Name and email are required.");
            }
            var supplier = await supplierService.CreateSupplierAsync(name, email);
            return Results.Created($"/api/suppliers/{supplier.Id}", supplier);
        });

        group.MapPut("/{id:int}", async (int id, string name, string email, ISupplierService supplierService) =>
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                return Results.BadRequest("Name and email are required.");
            }
            try
            {
                var supplier = await supplierService.UpdateSupplierAsync(id, name, email);
                return Results.Ok(supplier);
            }
            catch (ArgumentException)
            {
                return Results.NotFound();
            }
        });

        return group;
    }
}