﻿using Data.Entitites;

namespace Repository.Contracts
{
    public interface IOrderRepository : IRepository
    {
        Task<List<OrderDetailProduct>> GetOrderDetailProducts(int id);
        Task ComputeTotalPriceAsync(List<OrderDetailProduct> products);
        void ComputeTotalPriceForOrder(Order order);
        void CreateRangeOrder(Order order);
        void CreateUserOrder(Order order);
        Task<Order> CompleteOrder(int id);
    }
}
