namespace Souk.Api.Controllers;

public static class ProductController
{
    public static RouteGroupBuilder RouteProductController(this WebApplication app)
    {
        var group  = app.MapGroup("product");

        group.MapGet("/", () => { });
        
        group.MapGet("/{id}", () => { }).WithName("GetProduct");
        
        group.MapPost("/", () => { });

        return group;
    }
}