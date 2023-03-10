using ApplicationService.Contracts;
using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.RequestFeatures;

namespace ApplicationService.Implementations
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductRepository repository;

        public ProductManagementService(IProductRepository _repository) => repository = _repository;

        public async Task<IEnumerable<ProductDTO>> GetAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var products = repository.FindAll<Product>().ToListAsync();

                return ObjectMapper.Mapper.Map<List<ProductDTO>>(await products);
            }
            else
            {
                var products = await repository.FindByConditionAsync<Product>(x => x.ProductName.Contains(query));

                return ObjectMapper.Mapper.Map<List<ProductDTO>>(products);
            }
        }

        public async Task<ProductDTO> GetByIdAsync(long id)
        {
            var product = await repository.FindByIdAsync<Product>(id);

            if (product is null)
            {
                throw new NotFoundException(id, nameof(Product));
            }

            var productDto = ObjectMapper.Mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParametersAsync(ProductParameters productsParameters)
        {
            var productWithMetaData = await repository.GetProductsByParametersAsync(productsParameters);

            var productDTO = ObjectMapper.Mapper.Map<IEnumerable<ProductDTO>>(productWithMetaData);

            return (products: productDTO, metaData: productWithMetaData.MetaData);
        }

        public async Task<ProductDTO> SaveAsync(ProductDTO productDto)
        {
            Product product = ObjectMapper.Mapper.Map<Product>(productDto);

            await repository.CreateProduct(product, productDto.ImageFile);

            await repository.SaveChangesAsync();

            var productToReturn = ObjectMapper.Mapper.Map<ProductDTO>(product);

            return productToReturn;
        }

        public async Task<ProductDTO> UpdateAsync(ProductDTO productDto)
        {
            Product product;

            if (productDto.ImageFile == null)
            {
                product = ObjectMapper.Mapper.Map<Product>(productDto);

                repository.Update(product);

                await repository.SaveChangesAsync();

                var productToReturn = ObjectMapper.Mapper.Map<ProductDTO>(product);

                return productToReturn;
            }
            else
            {
                product = ObjectMapper.Mapper.Map<Product>(productDto);

                repository.UpdateProductWithImage(productDto.ImageFile, product);

                await repository.SaveChangesAsync();

                var productToReturn = ObjectMapper.Mapper.Map<ProductDTO>(product);

                return productToReturn;
            }
        }


        public async Task DeleteAsync(long id)
        {
            Product product = await repository.FindByIdAsync<Product>(id);

            if (product is null)
            {
                throw new NotFoundException(id, nameof(Product));
            }

            repository.Delete(product);

            await repository.SaveChangesAsync();
        }
    }
}
