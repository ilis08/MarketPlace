﻿using Data.Context;
using Data.Entitites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.CategoryRepo;
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
        private RepositoryContext context;

        public UnitOfWork(RepositoryContext _context)
        {
            context = _context;
        }

        private CategoryRepository categoryRepository;
        private ProductRepository productRepository;
        private OrderRepository orderRepository;

        public ProductRepository ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(context, new ProductImage());
                }
                return productRepository;
            }
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(context);
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

        public async Task SaveAsync()
        {
           await context.SaveChangesAsync();
        }
        
        #region IDisposable Support

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        public IEnumerator GetEnumerator()
        {
            yield return new NotImplementedException();
        }
    }
}
