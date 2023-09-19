using MarketPlace.Domain.Entitites;

namespace MarketPlace.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<Order>
{

}
