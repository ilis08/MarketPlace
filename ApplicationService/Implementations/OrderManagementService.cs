using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using ApplicationService.DTOs.OrderManagementDTOs.OrderSaveDTOs;
using ApplicationService.Mapper;
using AutoMapper;
using Data.Entitites;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class OrderManagementService
    {
        public UnitOfWork unitOfWork = null;

        public OrderManagementService()
        { 
            unitOfWork = new UnitOfWork();
        }

        public async Task<IEnumerable<OrderGetDTO>> Get()
        {
            List<OrderGetDTO> orders = new List<OrderGetDTO>();

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

        public bool Save(OrderDTO orderDTO)
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

                unitOfWork.Save();

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(OrderDTO orderDTO)
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

                    unitOfWork.Save();

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

        public bool Delete(int id)
        {
            var order = unitOfWork.OrderRepository.GetOrder(id);

            if (order != null)
            {
                unitOfWork.OrderRepository.Delete(order);
                unitOfWork.Save();
            }

            return true;
        }

    }
}

