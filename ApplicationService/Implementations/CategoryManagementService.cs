using ApplicationService.DTOs;
using Data.Context;
using Data.Entitites;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class CategoryManagementService
    {
        private UnitOfWork unitOfWork;

        public CategoryManagementService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<CategoryDTO>> Get(string query)
        {
            List<CategoryDTO> categoriesDto = new();

            if (query == null)
            {
                using (unitOfWork)
                {
                    foreach (var item in await unitOfWork.CategoryRepository.GetCategoriesAsync())
                    {
                        categoriesDto.Add(new CategoryDTO
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description
                        });
                    }
                }   
            }

                /*else
                {
                    foreach (var item in unitOfWork.CategoryRepository.Find())
                    {
                        categoriesDto.Add(new CategoryDTO
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description
                        });
                    }
                }*/

            return categoriesDto;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            using (unitOfWork)
            {
                Category category = await unitOfWork.CategoryRepository.GetCategorytByIdAsync(id);
                if (category != null)
                {
                    categoryDTO = new CategoryDTO
                    {
                        Id = category.Id,
                        Title = category.Title,
                        Description = category.Description
                    };
                }
            }
               
            return categoryDTO;
        }

        public async Task<bool> Save(CategoryDTO categoryDTO)
        {
            Category Category = new Category()
            {
                Id = categoryDTO.Id,
                Title = categoryDTO.Title,
                Description = categoryDTO.Description
            };

            try
            {

                if (categoryDTO.Id == 0)
                {
                    unitOfWork.CategoryRepository.Create(Category);
                }
                else
                {
                    unitOfWork.CategoryRepository.Update(Category);
                }

                await unitOfWork.SaveAsync();

                return true;
            }
            catch
            {
                System.Console.WriteLine(Category);
                return false;
            }
        }



        public async Task<bool> Delete(int id)
        {
            try
            {
                Category category = await unitOfWork.CategoryRepository.GetCategorytByIdAsync(id);
                unitOfWork.CategoryRepository.Delete(category);
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

