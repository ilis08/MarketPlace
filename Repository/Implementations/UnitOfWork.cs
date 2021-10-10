using Data.Context;
using Data.Entitites;
using Microsoft.AspNetCore.Hosting;
using Repository.Implementations.OrderRepo;
using Repository.Implementations.ProductRepo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable, IEnumerable
    {
        private Store4DBContext context = new Store4DBContext();

        private IWebHostEnvironment environment;

        private ProductRepository productRepository;
        private GenericRepository<Category> categoryRepository;
        private OrderRepository orderRepository;

        public ProductRepository ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context, environment);
                }
                return categoryRepository;
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new OrderRepository(context);
                }
                return orderRepository;
            }
        }

        public void Save()
        {
           context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerator GetEnumerator()
        {
            yield return new NotImplementedException();
        }
    }
}
