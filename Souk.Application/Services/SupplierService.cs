using Souk.Application.DTOs;
using Souk.Application.Ports;
using Souk.Domain.Inventory.Entities;
using Souk.Domain.Inventory.Repositories;
using Souk.Domain.Inventory.ValueObjects;

namespace Souk.Application.Services;

public class SupplierService(ISupplierRepository supplierRepository) : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;

    public async Task<SupplierDto?> GetSupplierAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        return supplier == null ? null : MapToDto(supplier);
    }

    public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync(int page, int pageSize, string? name)
    {
        var suppliers = await _supplierRepository.GetAllSuppliersAsync(page, pageSize, name);
        return suppliers.Select(MapToDto);
    }

    public async Task<SupplierDto> CreateSupplierAsync(string name, EmailAddress email)
    {
        var supplier = Supplier.Create(name, email);
        var added = await _supplierRepository.AddAsync(supplier);
        return MapToDto(added);
    }

    public async Task<SupplierDto> UpdateSupplierAsync(int id, string name, EmailAddress email)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        if (supplier == null)
        {
            throw new ArgumentException("Supplier not found.");
        }
        supplier.Update(name, email);
        var updated = await _supplierRepository.UpdateAsync(supplier);
        return MapToDto(updated);
    }

    private static SupplierDto MapToDto(Supplier supplier)
    {
        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            EmailAddress = supplier.EmailAddress
        };
    }
}