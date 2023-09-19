using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Repository.RequestFeatures;

namespace Repository.Contracts;

public interface IProductRepository : IRepository
{
    Task<PagedList<Product>> GetProductsByParametersAsync(ProductParameters productsParameters);

    Task CreateProduct(Product entity, IFormFile file);

    void UpdateProductWithImage(IFormFile file, Product product);
}
