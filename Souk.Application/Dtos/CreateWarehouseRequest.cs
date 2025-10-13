using System.ComponentModel.DataAnnotations;

namespace Souk.Application.DTOs;

public record CreateWarehouseRequest
(
    [Required] string Name ,
    [Required]string Location ,
     int Capacity 
);