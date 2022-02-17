using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Repository.Implementations;

namespace ApplicationService.Implementations
{
    public class OrderManagementService
    {
        private readonly UnitOfWork unitOfWork;

        public OrderManagementService(UnitOfWork _unitOfWork) => unitOfWork = _unitOfWork;

        public async Task<IEnumerable<OrderGetDTO>> Get()
        {
            List<OrderGetDTO> ordersToReturn = new();

            using (unitOfWork)
            {
                var orders = await unitOfWork.OrderRepository.GetOrders();

                var ordersDto = ObjectMapper.Mapper.Map<List<OrderGetDTO>>(orders);

                ordersToReturn.AddRange(ordersDto);
            }

            return ordersToReturn;
        }


        public async Task<OrderGetByIdDTO> GetById(int id)
        {
            OrderGetByIdDTO orderDTO = new();

            Order order = await unitOfWork.OrderRepository.GetOrder(id);

            if (order is null)
            {
                throw new NotFoundException(id, nameof(Order));
            }

            List<OrderDetailProduct> orderDetailProducts = await unitOfWork.OrderRepository.GetOrderDetailProducts(id);

            var orderDetailProductsDTO = ObjectMapper.Mapper.Map<List<OrderDetailProductByIdDTO>>(orderDetailProducts);

            if (order != null)
            {
                orderDTO = new OrderGetByIdDTO
                {
                    Id = order.Id,
                    PaymentType = order.PaymentType,
                    IsCompleted = order.IsCompleted,
                    TotalPrice = order.TotalPrice,
                    FullName = order.OrderDetailUser.Name + " " + order.OrderDetailUser.Surname,
                    Phone = order.OrderDetailUser.PhoneNumber,
                    OrderDetailProducts = orderDetailProductsDTO
                };
            }

            return orderDTO;
        }

        public async Task<OrderGetByIdDTO> Save(OrderDTO orderDTO)
        {
            List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

            await unitOfWork.OrderRepository.ComputeTotalPriceAsync(mapObject);

            Order order = new Order()
            {
                Id = orderDTO.OrderId,
                PaymentType = orderDTO.PaymentType,
                OrderDetailUser = orderDTO.OrderDetailUser,
                OrderDetailProduct = mapObject
            };

            unitOfWork.OrderRepository.ComputeTotalPriceForOrder(order);

            unitOfWork.OrderRepository.CreateUserOrder(order);
            unitOfWork.OrderRepository.CreateRangeOrder(order);

            if (order.Id == 0)
            {
                unitOfWork.OrderRepository.Create(order);
            }

            await unitOfWork.SaveAsync();

            var orderToReturn = ObjectMapper.Mapper.Map<OrderGetByIdDTO>(order);

            return orderToReturn;
        }

        public async Task<bool> Update(OrderDTO orderDTO)
        {
            if (orderDTO != null)
            {
                List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

                try
                {
                    Order order = new Order()
                    {
                        Id = orderDTO.OrderId,
                        PaymentType = orderDTO.PaymentType,
                        OrderDetailUser = orderDTO.OrderDetailUser,
                        OrderDetailProduct = mapObject
                    };

                    if (order.Id == 0)
                    {
                        unitOfWork.OrderRepository.Update(order);
                    }

                    await unitOfWork.SaveAsync();

                    return true;

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task CompleteOrderAsync(int id) => await unitOfWork.OrderRepository.CompleteOrder(id);

        public async Task Delete(int id)
        {
            var order = await unitOfWork.OrderRepository.GetOrder(id);

            if (order is null)
            {
                throw new NotFoundException(id, nameof(Order));
            }

            unitOfWork.OrderRepository.Delete(order);

            await unitOfWork.SaveAsync();
        }

    }
}

