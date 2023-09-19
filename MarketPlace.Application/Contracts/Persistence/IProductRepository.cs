using MarketPlace.Domain.Entitites;

namespace MarketPlace.Application.Contracts.Persistence;

public interface IProductRepository : IAsyncRepository<Product>
{

}
