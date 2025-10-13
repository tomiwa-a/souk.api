using Microsoft.AspNetCore.Mvc;
using Souk.Application.DTOs;
using Souk.Application.Ports;
using Souk.Domain.Inventory.ValueObjects;

namespace Souk.Api.Controllers;

public static class SupplierController
{
    public static RouteGroupBuilder RouteSupplierController(this WebApplication app)
    {
        var group = app.MapGroup("api/suppliers").WithParameterValidation();

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

        group.MapPost("/", async (CreateSupplierRequest request, ISupplierService supplierService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.EmailAddress))
            {
                return Results.BadRequest("Name and email are required.");
            }
            var supplier = await supplierService.CreateSupplierAsync(request.Name, request.EmailAddress);
            return Results.Created($"/api/suppliers/{supplier.Id}", supplier);
        });

        group.MapPut("/{id:int}", async (int id,  UpdateSupplierRequest request, ISupplierService supplierService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.EmailAddress))
            {
                return Results.BadRequest("Name and email are required.");
            }
            try
            {
                var supplier = await supplierService.UpdateSupplierAsync(id, request.Name, request.EmailAddress);
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