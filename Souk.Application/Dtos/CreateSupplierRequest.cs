using System.ComponentModel.DataAnnotations;

namespace Souk.Application.DTOs;

public record CreateSupplierRequest(
    [Required] string Name,
    [Required] string EmailAddress
);