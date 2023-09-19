using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace ApplicationService.Implementations;

public class OrderManagementService : IOrderManagementService
{
    private readonly IOrderRepository repository;

    public OrderManagementService(IOrderRepository _repository) => repository = _repository;

    public async Task<IEnumerable<OrderGetDTO>> GetAsync()
    {
        var orders = await repository.FindAll<Order>().ToListAsync();

        return ObjectMapper.Mapper.Map<List<OrderGetDTO>>(orders);
    }


    public async Task<OrderGetByIdDTO> GetByIdAsync(long id)
    {
        var order = await repository.FindByCondition<Order>(p => p.Id == id)
                                    .Include(x => x.OrderDetailProduct)
                                    .ThenInclude(p => p.Product)
                                    .SingleOrDefaultAsync();


        if (order is null)
        {
            throw new NotFoundException(id, nameof(Order));
        }

        var orderDetailProductsDTO = ObjectMapper.Mapper.Map<List<OrderDetailProductByIdDTO>>(order.OrderDetailProduct);

        var orderToReturn = new OrderGetByIdDTO
        {
            Id = order.Id,
            PaymentType = order.PaymentType,
            IsCompleted = order.IsCompleted,
            TotalPrice = order.TotalPrice,
            UserId = order.UserId,
            OrderDetailProducts = orderDetailProductsDTO
        };


        return orderToReturn;
    }

    public async Task<OrderGetByIdDTO> SaveAsync(OrderDTO orderDTO)
    {
        List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

        await repository.ComputeTotalPriceAsync(mapObject);

        Order order = new()
        {
            Id = orderDTO.OrderId,
            PaymentType = orderDTO.PaymentType,
            UserId = orderDTO.UserId,
            OrderDetailProduct = mapObject
        };

        repository.ComputeTotalPriceForOrder(order);

        repository.CreateRangeOrder(order);

        if (order.Id == 0)
        {
            await repository.CreateAsync(order);
        }

        await repository.SaveChangesAsync();

        var orderToReturn = ObjectMapper.Mapper.Map<OrderGetByIdDTO>(order);

        return orderToReturn;
    }

    public async Task<OrderGetByIdDTO> UpdateAsync(OrderDTO orderDTO)
    {
        List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

        Order order = new Order()
        {
            Id = orderDTO.OrderId,
            PaymentType = orderDTO.PaymentType,
            UserId = orderDTO.UserId,
            OrderDetailProduct = mapObject
        };

        repository.Update(order);

        await repository.SaveChangesAsync();

        var orderToReturn = ObjectMapper.Mapper.Map<OrderGetByIdDTO>(order);

        return orderToReturn;
    }

    public async Task<OrderGetByIdDTO> CompleteOrderAsync(long id)
    {
        var orderToComplete = await repository.CompleteOrder(id);

        return ObjectMapper.Mapper.Map<OrderGetByIdDTO>(orderToComplete);
    }

    public async Task DeleteAsync(long id)
    {
        Order order = await repository.FindByIdAsync<Order>(id);

        if (order is null)
        {
            throw new NotFoundException(id, nameof(Order));
        }

        repository.Delete(order);

        await repository.SaveChangesAsync();
    }

}

