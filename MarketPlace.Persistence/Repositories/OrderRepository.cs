using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;

namespace MarketPlace.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(MarketPlaceDbContext _dbContext) : base(_dbContext)
    {
    }
}
