using ApplicationService.Contracts;
using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Contracts;
using Repository.RequestFeatures;

namespace ApplicationService.Implementations;

public class ProductManagementService : IProductManagementService
{
    private readonly IProductRepository repository;

    public ProductManagementService(IProductRepository _repository) => repository = _repository;

    public async Task<IEnumerable<ProductDTO>> GetAsync(string query = " ")
    {
        IQueryable<Product> productsQuery = repository.FindAll<Product>();

        if (string.IsNullOrWhiteSpace(query))
        {
            productsQuery = productsQuery.Where(x => x.ProductName.Contains(query));
        }

        var products = await productsQuery.ToListAsync();

        return ObjectMapper.Mapper.Map<List<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetByIdAsync(long id)
    {
        Product product = await repository.FindByIdAsync<Product>(id);

        if (product is null)
        {
            throw new NotFoundException(id, nameof(Product));
        }

        var productDto = ObjectMapper.Mapper.Map<ProductDTO>(product);

        return productDto;
    }

    public async Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParametersAsync(ProductParameters productsParameters)
    {
        PagedList<Product> productWithMetaData = await repository.GetProductsByParametersAsync(productsParameters);

        IEnumerable<ProductDTO> productDTO = ObjectMapper.Mapper.Map<IEnumerable<ProductDTO>>(productWithMetaData);

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
        Product product = ObjectMapper.Mapper.Map<Product>(productDto);

        if (productDto.ImageFile == null)
        {
            repository.Update(product);
        }
        else
        {
            repository.UpdateProductWithImage(productDto.ImageFile, product);
        }

        await repository.SaveChangesAsync();

        var productToReturn = ObjectMapper.Mapper.Map<ProductDTO>(product);

        return productToReturn;
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
