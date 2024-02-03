using MarketPlace.Domain.Entitites.OrderNS;

namespace MarketPlace.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<Order>
{

}
