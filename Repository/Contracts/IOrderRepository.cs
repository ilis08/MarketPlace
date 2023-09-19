using Data.Entitites;

namespace Repository.Contracts;

public interface IOrderRepository : IRepository
{
    Task<List<OrderDetailProduct>> GetOrderDetailProducts(long id);
    Task ComputeTotalPriceAsync(List<OrderDetailProduct> products);
    void ComputeTotalPriceForOrder(Order order);
    void CreateRangeOrder(Order order);
    Task<Order> CompleteOrder(long id);
}
