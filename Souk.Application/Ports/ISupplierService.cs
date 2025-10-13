using Souk.Application.DTOs;
using Souk.Domain.Inventory.ValueObjects;

namespace Souk.Application.Ports;

public interface ISupplierService
{
    Task<SupplierDto?> GetSupplierAsync(int id);
    Task<IEnumerable<SupplierDto>> GetSuppliersAsync(int page, int pageSize, string? name);
    Task<SupplierDto> CreateSupplierAsync(string name, EmailAddress email);
    Task<SupplierDto> UpdateSupplierAsync(int id, string name, EmailAddress email);
}