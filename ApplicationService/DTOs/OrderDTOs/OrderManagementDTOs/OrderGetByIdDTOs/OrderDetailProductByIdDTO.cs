namespace ApplicationService.DTOs.OrderManagementDTOs.GetById
{
    public class OrderDetailProductByIdDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }
    }
}
