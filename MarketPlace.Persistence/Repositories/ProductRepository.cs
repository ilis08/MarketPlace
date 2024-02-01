using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;

namespace MarketPlace.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(MarketPlaceDbContext _dbContext) : base(_dbContext)
    {
    }
}
