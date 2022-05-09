using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;

namespace ApplicationService.Contracts
{
    public interface IOrderManagementService
    {
        Task<IEnumerable<OrderGetDTO>> Get();
        Task<OrderGetByIdDTO> GetById(int id);
        Task<OrderGetByIdDTO> Save(OrderDTO orderDTO);
        Task<OrderGetByIdDTO> Update(OrderDTO orderDTO);
        Task<OrderGetByIdDTO> CompleteOrderAsync(int id);
        Task Delete(int id);
    }
}
