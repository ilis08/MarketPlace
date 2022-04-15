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
        Task<IEnumerable<ProductDTO>> Get(string query);
        Task<ProductDTO> GetById(int id);
        Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParameters(ProductParameters productsParameters);
        Task<ProductDTO> Save(ProductDTO productDto);
        Task<ProductDTO> Update(ProductDTO productDto);
        Task Delete(int id);
    }
}
