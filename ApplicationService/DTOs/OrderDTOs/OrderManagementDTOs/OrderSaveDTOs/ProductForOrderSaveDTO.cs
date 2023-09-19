using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApplicationService.DTOs.OrderManagementDTOs.OrderSaveDTOs;

public class ProductForOrderSaveDTO
{
    public int Id { get; set; }
    [BindNever]
    public string ProductName { get; set; }
    [BindNever]
    public double Price { get; set; }
}
