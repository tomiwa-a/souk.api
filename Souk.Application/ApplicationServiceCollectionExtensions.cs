using Microsoft.Extensions.DependencyInjection;
using Souk.Application.Ports;
using Souk.Application.Services;

namespace Souk.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
        return services;

    }
}