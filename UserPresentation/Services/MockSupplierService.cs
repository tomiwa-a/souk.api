using Souk.Application.DTOs;

namespace UserPresentation.Services;

public class MockSupplierService
{
    private readonly List<SupplierDto> _suppliers = new()
    {
        new SupplierDto { Id = 1, Name = "TechCorp", EmailAddress = "contact@techcorp.com" },
        new SupplierDto { Id = 2, Name = "GadgetInc", EmailAddress = "info@gadgetinc.com" }
    };

    public Task<List<SupplierDto>> GetSuppliersAsync() => Task.FromResult(_suppliers);

    public Task<SupplierDto?> GetSupplierAsync(int id) => Task.FromResult(_suppliers.FirstOrDefault(s => s.Id == id));
}