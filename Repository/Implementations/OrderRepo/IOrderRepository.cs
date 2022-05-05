using Data.Entitites;
using Repository.Implementations.BaseRepo;

namespace Repository.Implementations.OrderRepo
{
    public interface IOrderRepository : IRepository
    {
        Task<List<OrderDetailProduct>> GetOrderDetailProducts(int id);
        Task ComputeTotalPriceAsync(List<OrderDetailProduct> products);
        void ComputeTotalPriceForOrder(Order order);
        void CreateRangeOrder(Order order);
        void CreateUserOrder(Order order);
        Task CompleteOrder(int id);
    }
}
