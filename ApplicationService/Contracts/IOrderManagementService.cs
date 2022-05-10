using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;

namespace ApplicationService.Contracts
{
    public interface IOrderManagementService
    {
        Task<IEnumerable<OrderGetDTO>> GetAsync();
        Task<OrderGetByIdDTO> GetByIdAsync(int id);
        Task<OrderGetByIdDTO> SaveAsync(OrderDTO orderDTO);
        Task<OrderGetByIdDTO> UpdateAsync(OrderDTO orderDTO);
        Task<OrderGetByIdDTO> CompleteOrderAsync(int id);
        Task DeleteAsync(int id);
    }
}
