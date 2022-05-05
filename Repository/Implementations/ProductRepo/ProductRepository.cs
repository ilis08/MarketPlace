using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Repository.RequestFeatures;
using Exceptions;
using Repository.Implementations.BaseRepo;
using Repository.Extensions;
using System.Linq;

namespace Repository.Implementations.ProductRepo
{
    public class ProductRepository : BaseRepo.Repository, IProductRepository
    {
        internal ProductImage imageService;

        public ProductRepository(RepositoryContext context, ProductImage _imageService) : base(context)
        {
            imageService = _imageService;
        }

        public async Task CreateProduct(Product product, IFormFile file)
        {
            product.Image = imageService.SaveImageAsync(file);

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

            var products =  await FindAll<Product>()
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
