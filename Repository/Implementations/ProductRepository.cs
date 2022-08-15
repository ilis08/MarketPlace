using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Repository.RequestFeatures;
using Exceptions;
using Repository.Extensions;
using System.Linq;
using Repository.Contracts;

namespace Repository.Implementations
{
    public class ProductRepository : Implementations.Repository, IProductRepository
    {
        private readonly IProductImage imageService;

        public ProductRepository(RepositoryContext context, IProductImage _imageService) : base(context)
        {
            imageService = _imageService;
        }

        public async Task CreateProduct(Product product, IFormFile file)
        {
            product.Image = imageService.SaveImage(file);

            await CreateAsync(product);
        }

        public void UpdateProductWithImage(IFormFile image, Product product)
        {
            product.Image = imageService.UpdateImage(image, product.Image);

            Update(product);
        }

        public async Task<PagedList<Product>> GetProductsByParametersAsync(ProductParameters productsParameters)
        {
            if (!productsParameters.ValidPriceRange)
            {
                throw new PriceRangeBadRequestException();
            }

            var count = await FindByCondition<Product>(x => x.Category.Title.Equals(productsParameters.Category))
                            .FilterProducts(productsParameters.Ordering, productsParameters.MinPrice, productsParameters.MaxPrice)
                            .CountAsync();

            var products = await FindAll<Product>()
                            .FilterProducts(productsParameters.Ordering, productsParameters.MinPrice, productsParameters.MaxPrice)
                            .SearchByName(productsParameters.ProductName)
                            .SearchByCategory(productsParameters.Category)
                            .Skip((productsParameters.PageNumber - 1) * productsParameters.PageSize)
                            .Take(productsParameters.PageSize)
                          .ToListAsync();

            return new PagedList<Product>(products, count, productsParameters.PageNumber, productsParameters.PageSize);
        }
    }
}
