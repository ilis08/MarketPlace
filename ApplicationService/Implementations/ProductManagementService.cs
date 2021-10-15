using ApplicationService.DTOs;
using Data.Context;
using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class ProductManagementService
    {
        private UnitOfWork unitOfWork;

        public ProductManagementService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<ProductDTO>> Get(string query)
        {
            List<ProductDTO> productDto = new List<ProductDTO>();

            if (query == null)
            {
                using (unitOfWork)
                {
                    foreach (var item in await unitOfWork.ProductRepository.GetProducts())
                    {
                        productDto.Add(new ProductDTO
                        {
                            Id = item.Id,
                            ProductName = item.ProductName,
                            Description = item.Description,
                            Release = item.Release,
                            Price = item.Price,
                            Image = item.Image,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }
                        });
                    }
                }
            }
            else
            {
                using (unitOfWork)
                {
                    foreach (var item in unitOfWork.ProductRepository.GetProductByQuery(query))
                    {
                        productDto.Add(new ProductDTO
                        {
                            Id = item.Id,
                            ProductName = item.ProductName,
                            Description = item.Description,
                            Release = item.Release,
                            Price = item.Price,
                            Image = item.Image,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }
                        });
                    }
                }
            }           
            return productDto;
        }

        public async Task<IEnumerable<ProductDTO>> GetPaginated(GetProductsParameters parameters)
        {
            List<ProductDTO> productDto = new List<ProductDTO>();

            using (unitOfWork)
            {
                foreach (var item in await unitOfWork.ProductRepository.GetProductsWithParamsAsync(parameters))
                {
                    productDto.Add(new ProductDTO
                    {
                        Id = item.Id,
                        ProductName = item.ProductName,
                        Description = item.Description,
                        Release = item.Release,
                        Price = item.Price,
                        Image = item.Image,
                        Category = new CategoryDTO
                        {
                            Id = item.Category.Id,
                            Title = item.Category.Title,
                            Description = item.Category.Description
                        }
                    });
                }
            }

            return productDto;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            ProductDTO productDto = new ProductDTO();

            Product product = await unitOfWork.ProductRepository.GetProductById(id);

            if (product != null)
            {
                productDto = new ProductDTO
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Release = product.Release,
                    Price = product.Price,
                    Image = product.Image,
                    Category = new CategoryDTO
                    {
                        Id = product.Category.Id,
                        Title = product.Category.Title,
                        Description = product.Category.Description
                    }
                };
            }

            return productDto;
        }

        public async Task<bool> Save(ProductDTO productDto)
        { 
            try
            {
                productDto.Image = unitOfWork.ProductRepository.SaveImageAsync(productDto.ImageFile);

                Product product = new Product
                {
                    Id = productDto.Id,
                    ProductName = productDto.ProductName,
                    Description = productDto.Description,
                    Release = productDto.Release,
                    Price = productDto.Price,
                    Image = productDto.Image,
                    CategoryId = productDto.CategoryId
                };

                if (productDto.Id == 0)
                {
                    unitOfWork.ProductRepository.Create(product);
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
                    Product product = new Product
                    {
                        Id = productDto.Id,
                        ProductName = productDto.ProductName,
                        Description = productDto.Description,
                        Release = productDto.Release,
                        Price = productDto.Price,
                        Image = productDto.Image,
                        CategoryId = productDto.CategoryId
                    };

                    using (unitOfWork)
                    {
                        unitOfWork.ProductRepository.Update(product);

                        await unitOfWork.SaveAsync();
                    }
                }
                else
                {
                    productDto.Image = unitOfWork.ProductRepository.UpdateImage(productDto.ImageFile, productDto.Image);

                    Product product = new Product
                    {
                        Id = productDto.Id,
                        ProductName = productDto.ProductName,
                        Description = productDto.Description,
                        Release = productDto.Release,
                        Price = productDto.Price,
                        Image = productDto.Image,
                        CategoryId = productDto.CategoryId
                    };

                    using (unitOfWork)
                    {
                        unitOfWork.ProductRepository.Update(product);

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


        public async Task<bool> Delete(int id)
        {
            try
            {
                Product product = await unitOfWork.ProductRepository.GetProductById(id);
                unitOfWork.ProductRepository.Delete(product);
                await unitOfWork.SaveAsync();


                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
