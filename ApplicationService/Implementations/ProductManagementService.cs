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
        public async Task<IEnumerable<ProductDTO>> Get(string query)
        {
            List<ProductDTO> productDto = new List<ProductDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (query == null)
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
               /* else
                {
                    foreach (var item in unitOfWork.ProductRepository.GetByQuery().Where(c => c.ProductName.Contains(query)).ToList())
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
                }*/
            }
            return productDto;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            ProductDTO productDto = new ProductDTO();

                using(UnitOfWork unitOfWork = new UnitOfWork())
                {
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
                }

            return productDto;
        }

        public bool Save(ProductDTO productDto)
        { 
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
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
                    else
                    {
                        unitOfWork.ProductRepository.Update(product);
                    }
                    unitOfWork.Save();
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
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Product product = await unitOfWork.ProductRepository.GetProductById(id);
                    unitOfWork.ProductRepository.Delete(product);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
