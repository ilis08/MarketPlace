using ApplicationService.DTOs.ProductDTOs;
using Repository.RequestFeatures;

namespace ApplicationService.Contracts
{
    public interface IProductManagementService
    {
        Task<IEnumerable<ProductDTO>> GetAsync(string query);
        Task<ProductDTO> GetByIdAsync(long id);
        Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParametersAsync(ProductParameters productsParameters);
        Task<ProductDTO> SaveAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateAsync(ProductDTO productDto);
        Task DeleteAsync(long id);
    }
}
