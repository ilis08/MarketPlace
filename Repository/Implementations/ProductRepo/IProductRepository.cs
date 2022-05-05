using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Repository.Implementations.BaseRepo;
using Repository.RequestFeatures;

namespace Repository.Implementations.ProductRepo
{
    public interface IProductRepository : IRepository
    {
        Task<PagedList<Product>> GetProductsByParametersAsync(ProductParameters productsParameters);

        Task CreateProduct(Product entity, IFormFile file);

        void UpdateProductWithImage(IFormFile file, Product product);
    }
}
