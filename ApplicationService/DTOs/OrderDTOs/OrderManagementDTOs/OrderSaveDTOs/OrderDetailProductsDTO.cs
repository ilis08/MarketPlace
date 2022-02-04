using ApplicationService.DTOs.OrderManagementDTOs.OrderSaveDTOs;
using System.ComponentModel.DataAnnotations;

namespace ApplicationService.DTOs.OrderManagementDTOs
{
    public class OrderDetailProductsDTO
    { 
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }

        public int ProductId { get; set; }

        public ProductForOrderSaveDTO Product { get; set; }
    }
}
