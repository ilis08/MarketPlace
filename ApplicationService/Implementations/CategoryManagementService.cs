using ApplicationService.DTOs;
using Data.Context;
using Data.Entitites;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Implementations
{
    public class CategoryManagementService
    {
        public List<CategoryDTO> Get(string query)
        {
            List<CategoryDTO> categoriesDto = new();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (query == null)
                {
                    foreach (var item in unitOfWork.CategoryRepository.Get())
                    {
                        categoriesDto.Add(new CategoryDTO
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description
                        });
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
            }
            return categoriesDto;
        }

        public CategoryDTO GetById(int id)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Category category = unitOfWork.CategoryRepository.GetById(id);
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

        public bool Save(CategoryDTO categoryDTO)
        {
            Category Category = new Category()
            {
                Id = categoryDTO.Id,
                Title = categoryDTO.Title,
                Description = categoryDTO.Description
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (categoryDTO.Id == 0)
                    {
                        unitOfWork.CategoryRepository.Create(Category);
                    }
                    else
                    {
                        unitOfWork.CategoryRepository.Update(Category);
                    }
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                System.Console.WriteLine(Category);
                return false;
            }
        }



        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Category category = unitOfWork.CategoryRepository.GetById(id);
                    unitOfWork.CategoryRepository.Delete(category);
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

