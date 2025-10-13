using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class ApiProductService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SoukApi");

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/products?page=1&pageSize=1000");
        return response?.ToList() ?? new List<ProductDto>();
    }

    public async Task<ProductDto?> GetProductAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{id}");
    }

    public async Task<List<ProductDto>> GetProductsBySupplierAsync(int supplierId)
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/products/supplier/{supplierId}");
        return response?.ToList() ?? new List<ProductDto>();
    }

    public async Task UpdateProductQuantityAsync(int id, int newQuantity)
    {
        // Get current product to know warehouse and current quantity
        var product = await GetProductAsync(id);
        if (product == null) return;

        int delta = newQuantity - product.Quantity;
        if (delta == 0) return;

        string endpoint = delta > 0
            ? $"api/warehouses/{product.WarehouseId}/products/{id}/increase"
            : $"api/warehouses/{product.WarehouseId}/products/{id}/decrease";

        var content = JsonContent.Create(new { quantity = Math.Abs(delta) });
        await _httpClient.PutAsync(endpoint, content);
    }
}