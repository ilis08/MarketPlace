using ApplicationService.DTOs.OrderManagementDTOs;
using Data.Entitites;

namespace ApplicationService.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public PaymentType PaymentType { get; set; } 

        public double TotalPrice { get; set; }

        public OrderDetailUser OrderDetailUser { get; set; }

        public IEnumerable<OrderDetailProductsDTO> OrderDetailProducts { get; set; }

    }
}
