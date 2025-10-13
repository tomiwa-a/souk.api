using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Souk.Domain.Inventory.Repositories;
using Souk.Infrastructure.Data;
using Souk.Infrastructure.Data.Repository;

namespace Souk.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {

        services.AddDbContext<SoukDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("Souk.Api")));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IPurchaseOrdersRepository, PurchaseOrdersRepository>();

        return services;
    }
}