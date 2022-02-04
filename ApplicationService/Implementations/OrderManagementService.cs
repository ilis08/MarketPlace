using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using ApplicationService.Mapper;
using Data.Entitites;
using Repository.Implementations;

namespace ApplicationService.Implementations
{
    public class OrderManagementService
    {
        private readonly UnitOfWork unitOfWork;

        public OrderManagementService(UnitOfWork _unitOfWork)
        { 
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<OrderGetDTO>> Get()
        {
            List<OrderGetDTO> orders = new();

            using (unitOfWork)
            {
                foreach (var item in await unitOfWork.OrderRepository.GetOrders())
                {
                    orders.Add(new OrderGetDTO
                    {
                        Id = item.Id,
                        PaymentType = item.PaymentType,
                        IsCompleted = item.IsCompleted,
                        TotalPrice = item.TotalPrice
                    });
                }
            }

            return orders;
        }


        public async Task<OrderGetByIdDTO> GetById(int id)
        {
            OrderGetByIdDTO orderDTO = new OrderGetByIdDTO();

            Order order = await unitOfWork.OrderRepository.GetOrder(id);

            List<OrderDetailProduct> orderDetailProducts = unitOfWork.OrderRepository.GetOrderDetailProducts(id);

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

        public async Task<bool> Save(OrderDTO orderDTO)
        {
            List<OrderDetailProduct> mapObject = ObjectMapper.Mapper.Map<List<OrderDetailProduct>>(orderDTO.OrderDetailProducts);

            unitOfWork.OrderRepository.ComputeTotalPrice(mapObject);

            try
            { 
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

                return true;

            }
            catch (Exception)
            {
                throw;
            }
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

        public async Task<bool> CompleteOrderAsync(int id)
        {
            try
            {
                await unitOfWork.OrderRepository.CompleteOrder(id);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var order = unitOfWork.OrderRepository.GetOrder(id);

            if (order != null)
            {
                unitOfWork.OrderRepository.Delete(order);

                await unitOfWork.SaveAsync();
            }

            return true;
        }

    }
}

