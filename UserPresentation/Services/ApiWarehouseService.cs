using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class ApiWarehouseService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SoukApi");

    public async Task<List<WarehouseDto>> GetWarehousesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<WarehouseDto>>("api/warehouses?page=1&pageSize=1000");
        return response?.ToList() ?? new List<WarehouseDto>();
    }

    public async Task<WarehouseDto?> GetWarehouseAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<WarehouseDto>($"api/warehouses/{id}");
    }

    public async Task<List<ProductDto>> GetProductsByWarehouseAsync(int warehouseId)
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/products/warehouse/{warehouseId}");
        return response?.ToList() ?? new List<ProductDto>();
    }

    public async Task<List<PurchaseOrderDto>> GetPurchaseOrdersByWarehouseAsync(int warehouseId)
    {
        // Assuming there's an endpoint, but from earlier, PurchaseOrderController doesn't have by warehouse.
        // For now, return empty or implement later.
        return new List<PurchaseOrderDto>();
    }

    public async Task CreateWarehouseAsync(CreateWarehouseRequest request)
    {
        var content = JsonContent.Create(request);
        await _httpClient.PostAsync("api/warehouses", content);
    }

    public async Task UpdateWarehouseAsync(int id, UpdateWarehouseRequest request)
    {
        var content = JsonContent.Create(request);
        await _httpClient.PutAsync($"api/warehouses/{id}", content);
    }

    public async Task AddProductToWarehouseAsync(int warehouseId, CreateProductRequest request)
    {
        var content = JsonContent.Create(request);
        await _httpClient.PostAsync($"api/warehouses/{warehouseId}/products", content);
    }

    public async Task CreatePurchaseOrderAsync(int warehouseId, CreatePurchaseOrderRequest request)
    {
        var content = JsonContent.Create(request);
        await _httpClient.PostAsync($"api/warehouses/{warehouseId}/purchase-orders", content);
    }
}