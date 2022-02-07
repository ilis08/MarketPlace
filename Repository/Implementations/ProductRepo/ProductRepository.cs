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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        internal ProductImage imageService;

        public ProductRepository(RepositoryContext context, ProductImage _imageService) : base(context)
        {
            imageService = _imageService;
        }

        public void CreateProduct(Product product, IFormFile file)
        {
            product.Image = imageService.SaveImageAsync(file);

            Create(product);
        }

        public void DeleteProduct(Product product) => Delete(product);

        public async Task<List<Product>> GetProductByQuery(string query) => 
            await FindByCondition(c => c.ProductName.Contains(query)).Include(x => x.Category).ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id) =>
            await FindByCondition(c => c.Id == id).Include(x => x.Category).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductsAsync() =>
            await FindAll().Include(x => x.Category).OrderBy(x => x.Category.Id).ToListAsync();

        public void UpdateProduct(Product product) => Update(product);

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

            var count = await FindByCondition(x => x.Category.Title.Equals(productsParameters.Category))
                            .FilterProducts(productsParameters.Ordering, productsParameters.MinPrice, productsParameters.MaxPrice)
                            .CountAsync();

            var products =  await FindAll()
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
