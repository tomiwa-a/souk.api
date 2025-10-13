
using Souk.Api.Controllers;
using Souk.Api.Middleware;
using Souk.Application;
using Souk.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddInfrastructureServices(connectionString);
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome to Souk.Api");

app.RouteProductController();
app.RouteSupplierController();
app.RouteWarehouseController();
app.RoutePurchaseOrderController();

app.UseCors("AllowAll");

app.Run();