using Data.Entitites;

namespace Repository.Implementations.OrderRepo
{
    interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();

        Task<Order> GetOrder(int id);
    }
}
