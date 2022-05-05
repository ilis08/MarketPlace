using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations;
using Repository.Implementations.OrderRepo;

namespace ApplicationService.Implementations
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly IOrderRepository repository;

        public OrderManagementService(IOrderRepository _repository) => repository = _repository;

        public async Task<IEnumerable<OrderGetDTO>> Get()
        {
            var orders = await repository.FindAll<Order>().ToListAsync();

            return ObjectMapper.Mapper.Map<List<OrderGetDTO>>(orders);
        }


        public async Task<OrderGetByIdDTO> GetById(int id)
        {
            var order = await repository.FindByCondition<Order>(p => p.Id == id)
                                        .Include(x => x.OrderDetailUser)
                                        .Include(x => x.OrderDetailProduct)
                                        .ThenInclude(p => p.Product)
                                        .SingleOrDefaultAsync();


            if (order is null)
            {
                throw new NotFoundException(id, nameof(Order));
            }

            var orderDetailProducts = await repository.FindByCondition<OrderDetailProduct>(x => x.OrderId == order.Id).ToListAsync();

            var orderDetailProductsDTO = ObjectMapper.Mapper.Map<List<OrderDetailProductByIdDTO>>(orderDetailProducts);

            var orderToReturn = new OrderGetByIdDTO
            {
                Id = order.Id,
                PaymentType = order.PaymentType,
                IsCompleted = order.IsCompleted,
                TotalPrice = order.TotalPrice,
                FullName = order.OrderDetailUser.Name + " " + order.OrderDetailUser.Surname,
                Phone = order.OrderDetailUser.PhoneNumber,
                OrderDetailProducts = orderDetailProductsDTO
            };


            return orderToReturn;
        }

        public async Task<OrderGetByIdDTO> Save(OrderDTO orderDTO)
        {
            List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

            await repository.ComputeTotalPriceAsync(mapObject);

            Order order = new()
            {
                Id = orderDTO.OrderId,
                PaymentType = orderDTO.PaymentType,
                OrderDetailUser = orderDTO.OrderDetailUser,
                OrderDetailProduct = mapObject
            };

            repository.ComputeTotalPriceForOrder(order);

            repository.CreateUserOrder(order);
            repository.CreateRangeOrder(order);

            if (order.Id == 0)
            {
                await repository.CreateAsync(order);
            }

            await repository.SaveChangesAsync();

            var orderToReturn = ObjectMapper.Mapper.Map<OrderGetByIdDTO>(order);

            return orderToReturn;
        }

        public async Task<bool> Update(OrderDTO orderDTO)
        {
            if (orderDTO is null)
            {
                throw new NotFoundException(orderDTO.OrderId, nameof(Order));
            }

            List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

            Order order = new Order()
            {
                Id = orderDTO.OrderId,
                PaymentType = orderDTO.PaymentType,
                OrderDetailUser = orderDTO.OrderDetailUser,
                OrderDetailProduct = mapObject
            };

            repository.Update(order);

            await repository.SaveChangesAsync();

            return true;
        }

        public async Task CompleteOrderAsync(int id) => await repository.CompleteOrder(id);

        public async Task Delete(int id)
        {
            var order = await repository.FindByCondition<Order>(x => x.Id == id).FirstOrDefaultAsync();

            if (order is null)
            {
                throw new NotFoundException(id, nameof(Order));
            }

            repository.Delete(order);

            await repository.SaveChangesAsync();
        }

    }
}

