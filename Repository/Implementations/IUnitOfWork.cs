using Repository.Implementations.CategoryRepo;
using Repository.Implementations.OrderRepo;
using Repository.Implementations.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public interface IUnitOfWork: IDisposable
    {
        public CategoryRepository CategoryRepository { get; }
        public ProductRepository ProductRepository { get; }      
        public OrderRepository OrderRepository { get; }
        Task SaveChangesAsync();
    }
}
