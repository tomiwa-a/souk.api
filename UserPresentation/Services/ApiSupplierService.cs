using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class ApiSupplierService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SoukApi");

    public async Task<List<SupplierDto>> GetSuppliersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<SupplierDto>>("api/suppliers?page=1&pageSize=1000");
        return response?.ToList() ?? new List<SupplierDto>();
    }

    public async Task<SupplierDto?> GetSupplierAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<SupplierDto>($"api/suppliers/{id}");
    }

    public async Task CreateSupplierAsync(CreateSupplierRequest request)
    {
        var content = JsonContent.Create(request);
        await _httpClient.PostAsync("api/suppliers", content);
    }

    public async Task UpdateSupplierAsync(int id, UpdateSupplierRequest request)
    {
        var content = JsonContent.Create(request);
        await _httpClient.PutAsync($"api/suppliers/{id}", content);
    }
}