using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Repository.Implementations;
using Repository.RequestFeatures;

namespace ApplicationService.Implementations
{
    public class ProductManagementService
    {
        private readonly UnitOfWork unitOfWork;

        public ProductManagementService(UnitOfWork _unitOfWork) => unitOfWork = _unitOfWork;

        public async Task<IEnumerable<ProductDTO>> Get(string query)
        {
            List<ProductDTO> productDto = new();

            if (string.IsNullOrWhiteSpace(query))
            {
                using (unitOfWork)
                {
                    var products = unitOfWork.ProductRepository.GetProductsAsync();

                    productDto = ObjectMapper.Mapper.Map<List<ProductDTO>>(await products);
                }
            }
            else
            {
                using (unitOfWork)
                {
                    var products = unitOfWork.ProductRepository.GetProductByQuery(query);

                    productDto = ObjectMapper.Mapper.Map<List<ProductDTO>>(await products);
                }
            }
            return productDto;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            ProductDTO productDto = new();

            Product product = await unitOfWork.ProductRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException(id, nameof(Product));
            }

            productDto = ObjectMapper.Mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<(IEnumerable<ProductDTO> products, MetaData metaData)> GetProductsByParameters(ProductParameters productsParameters)
        {
            var productWithMetaData = await unitOfWork.ProductRepository.GetProductsByParametersAsync(productsParameters);

            var productDTO = ObjectMapper.Mapper.Map<IEnumerable<ProductDTO>>(productWithMetaData);

            return (products: productDTO, metaData: productWithMetaData.MetaData);
        }

        public async Task<bool> Save(ProductDTO productDto)
        {
            try
            {
                Product product = ObjectMapper.Mapper.Map<Product>(productDto);

                if (productDto.Id == 0)
                {
                    unitOfWork.ProductRepository.CreateProduct(product, productDto.ImageFile);
                }

                await unitOfWork.SaveAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(ProductDTO productDto)
        {
            try
            {
                if (productDto.ImageFile == null)
                {
                    Product product = ObjectMapper.Mapper.Map<Product>(productDto);

                    using (unitOfWork)
                    {
                        unitOfWork.ProductRepository.Update(product);

                        await unitOfWork.SaveAsync();
                    }
                }
                else
                {
                    Product product = ObjectMapper.Mapper.Map<Product>(productDto);

                    using (unitOfWork)
                    {
                        unitOfWork.ProductRepository.UpdateProductWithImage(productDto.ImageFile, product);

                        await unitOfWork.SaveAsync();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task Delete(int id)
        {
            Product product = await unitOfWork.ProductRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException(id, nameof(Product));
            }

            unitOfWork.ProductRepository.Delete(product);
            await unitOfWork.SaveAsync();
        }
    }
}
