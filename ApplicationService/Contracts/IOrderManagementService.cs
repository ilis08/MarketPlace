using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Contracts
{
    public interface IOrderManagementService
    {
        Task<IEnumerable<OrderGetDTO>> Get();
        Task<OrderGetByIdDTO> GetById(int id);
        Task<OrderGetByIdDTO> Save(OrderDTO orderDTO);
        Task<bool> Update(OrderDTO orderDTO);
        Task CompleteOrderAsync(int id);
        Task Delete(int id);
    }
}
