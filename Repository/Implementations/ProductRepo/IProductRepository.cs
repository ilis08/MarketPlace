using Data.Entitites;
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

        void Create(Product entity);

        void Update(Product entity);

        void Delete(Product entity);
    }
}
