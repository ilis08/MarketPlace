using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using Data.Entitites;

namespace ApplicationService.DTOs.OrderManagementDTOs
{
    public class OrderGetByIdDTO
    {
        public long Id { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsCompleted { get; set; }

        public double TotalPrice { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public List<OrderDetailProductByIdDTO> OrderDetailProducts { get; set; }
    }
}
