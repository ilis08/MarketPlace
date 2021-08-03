using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
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

        public List<OrderGetDTO> Get()
        {
            List<OrderGetDTO> orders = new List<OrderGetDTO>();

            foreach (var item in unitOfWork.OrderRepository.GetOrders())
            {
                orders.Add(new OrderGetDTO
                {
                    Id = item.Id,
                    PaymentType = item.PaymentType
                });
            }

            return orders;
        }

        public OrderGetByIdDTO GetById(int id)
        {
            OrderGetByIdDTO orderDTO = new OrderGetByIdDTO();

            Order order = unitOfWork.OrderRepository.GetOrder(id);

            List<OrderDetailProduct> orderDetailProducts = unitOfWork.OrderDetailProductRepository.GetOrderDetailProducts(id);

            var orderDetailProductsDTO = ObjectMapper.Mapper.Map<List<OrderDetailProductByIdDTO>>(orderDetailProducts);

            if (order != null)
            {
                orderDTO = new OrderGetByIdDTO
                {
                    Id = order.Id,
                    PaymentType = order.PaymentType,
                    FullName = order.OrderDetailUser.Name + order.OrderDetailUser.Surname,
                    Phone = order.OrderDetailUser.PhoneNumber,
                    OrderDetailProducts = orderDetailProductsDTO
                };
            }

            return orderDTO;
        }

        public bool Save(OrderDTO orderDTO)
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


        public bool Delete(int id)
        {
            var order = unitOfWork.OrderRepository.GetById(id);

            if (order != null)
            {
                unitOfWork.OrderRepository.Delete(order);
                unitOfWork.Save();
            }

            return true;
        }

    }
}

