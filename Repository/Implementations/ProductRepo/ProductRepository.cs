using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;

namespace Repository.Implementations.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        internal RepositoryContext context;
        internal ProductImage imageService;

        public ProductRepository(RepositoryContext context, ProductImage _imageService)
        {
            this.context = context;
            imageService = _imageService;
        }

        public void Create(Product product, IFormFile file)
        {
            product.Image = imageService.SaveImageAsync(file);

            context.Add(product);
        }

        public void Delete(Product entity)
        {
            context.Remove(entity);
        }

        public IQueryable<Product> GetProductByQuery(string query)
        {
            return context.Products.Include(x => x.Category).Where(c => c.ProductName.Contains(query));
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.Where(p => p.Id == id).Include(x => x.Category).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context.Products.Include(x => x.Category).OrderBy(x => x.Category.Id).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsWithParamsAsync(GetProductsParameters getProducts)
        {
            return await context.Products.OrderBy(p => p.Price).Skip((getProducts.PageNumber - 1) * getProducts.PageSize).Take(getProducts.PageSize).Include(x => x.Category).ToListAsync();
        }

        public void Update(Product entity)
        {
            context.Products.Update(entity);
        }

        public void UpdateWithImage(IFormFile image, Product product)
        {
            product.Image = imageService.UpdateImage(image, product.Image);

            context.Products.Update(product);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(GetProductsParameters productsParameters)
        {
            if (productsParameters.Ordering == Ordering.OrderBy)
            {
                return await context.Products.Where(p => p.Category.Title == productsParameters.Category).OrderBy(x => x.Price).ToListAsync();
            }
            else
            {
                return await context.Products.Where(p => p.Category.Title == productsParameters.Category).OrderByDescending(x => x.Price).ToListAsync();
            }
            
        }
    }
}
