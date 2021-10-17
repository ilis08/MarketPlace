using Data.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.ProductRepo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProductById(int id);

        Task<IEnumerable<Product>> GetProductByCategory(GetProductsParameters productsParameters);

        void Create(Product entity, IFormFile file);

        void Update(Product entity);

        void UpdateWithImage(IFormFile file, Product product);

        void Delete(Product entity);
    }
}
