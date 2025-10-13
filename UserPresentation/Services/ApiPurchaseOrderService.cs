using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class ApiPurchaseOrderService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SoukApi");

    public async Task<List<PurchaseOrderDto>> GetPurchaseOrdersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<PurchaseOrderDto>>("api/purchase-orders?page=1&pageSize=1000");
        return response?.ToList() ?? new List<PurchaseOrderDto>();
    }

    public async Task<PurchaseOrderDto?> GetPurchaseOrderAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PurchaseOrderDto>($"api/purchase-orders/{id}");
    }
}