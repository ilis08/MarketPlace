using ApplicationService.DTOs.ProductDTOs;
using Repository.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Contracts
{
    public interface IProductManagementService
    {
        Task<IEnumerable<ProductDTO>> GetAsync(string query);
        Task<ProductDTO> GetByIdAsync(int id);
        Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParametersAsync(ProductParameters productsParameters);
        Task<ProductDTO> SaveAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateAsync(ProductDTO productDto);
        Task DeleteAsync(int id);
    }
}
