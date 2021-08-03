using Data.Context;
using Data.Entitites;
using Microsoft.AspNetCore.Hosting;
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
        private Store3DBContext context = new Store3DBContext();

        private IWebHostEnvironment environment;

        private GenericRepository<Product> productRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<OrderDetailProduct> orderDetailProductRepository;
        private GenericRepository<OrderDetailUser> orderDetailUserRepository;

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context, environment);
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

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context, environment);
                }
                return orderRepository;
            }
        }
        public GenericRepository<OrderDetailProduct> OrderDetailProductRepository
        {
            get
            {
                if (this.orderDetailProductRepository == null)
                {
                    this.orderDetailProductRepository = new GenericRepository<OrderDetailProduct>(context, environment);
                }
                return orderDetailProductRepository;
            }
        }
        public GenericRepository<OrderDetailUser> OrderDetailUserRepository
        {
            get
            {
                if (this.orderDetailUserRepository == null)
                {
                    this.orderDetailUserRepository = new GenericRepository<OrderDetailUser>(context, environment);
                }
                return orderDetailUserRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
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
