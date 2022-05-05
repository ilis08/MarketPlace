using ApplicationService.Contracts;
using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations;
using Repository.Implementations.ProductRepo;
using Repository.RequestFeatures;

namespace ApplicationService.Implementations
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductRepository repository;

        public ProductManagementService(IProductRepository _repository) => repository = _repository;

        public async Task<IEnumerable<ProductDTO>> Get(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var products = repository.FindAll<Product>().ToListAsync();

                return ObjectMapper.Mapper.Map<List<ProductDTO>>(await products);
            }
            else
            {
                var products = repository.FindByCondition<Product>(x => x.ProductName.Contains(query)).ToListAsync();

                return ObjectMapper.Mapper.Map<List<ProductDTO>>(await products);
            }
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await repository.FindByCondition<Product>(x => x.Id == id).FirstOrDefaultAsync();

            if (product is null)
            {
                throw new NotFoundException(id, nameof(Product));
            }

            var productDto = ObjectMapper.Mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParameters(ProductParameters productsParameters)
        {
            var productWithMetaData = await repository.GetProductsByParametersAsync(productsParameters);

            var productDTO = ObjectMapper.Mapper.Map<IEnumerable<ProductDTO>>(productWithMetaData);

            return (products: productDTO, metaData: productWithMetaData.MetaData);
        }

        public async Task<ProductDTO> Save(ProductDTO productDto)
        {
            Product product = ObjectMapper.Mapper.Map<Product>(productDto);

            await repository.CreateProduct(product, productDto.ImageFile);

            await repository.SaveChangesAsync();

            var productToReturn = ObjectMapper.Mapper.Map<ProductDTO>(product);

            return productToReturn;
        }

        public async Task<ProductDTO> Update(ProductDTO productDto)
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


        public async Task Delete(int id)
        {
            Product product = await repository.FindByCondition<Product>(x => x.Id == id).FirstOrDefaultAsync();

            if (product is null)
            {
                throw new NotFoundException(id, nameof(Product));
            }

            repository.Delete(product);

            await repository.SaveChangesAsync();
        }
    }
}
