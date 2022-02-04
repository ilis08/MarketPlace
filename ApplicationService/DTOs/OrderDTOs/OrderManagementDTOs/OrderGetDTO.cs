using Data.Entitites;

namespace ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs
{
    public class OrderGetDTO
    {
        public int Id { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsCompleted { get; set; }

        public double TotalPrice { get; set; }
    }
}
