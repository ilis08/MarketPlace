using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Repository.RequestFeatures;

namespace Repository.Implementations.ProductRepo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<PagedList<Product>> GetProductsByParametersAsync(ProductParameters productsParameters);

        Task<List<Product>> GetProductByQuery(string query);

        void CreateProduct(Product entity, IFormFile file);

        void UpdateProduct(Product entity);

        void UpdateProductWithImage(IFormFile file, Product product);

        void DeleteProduct(Product entity);
    }
}
