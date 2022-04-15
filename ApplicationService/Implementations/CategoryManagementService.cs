using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Repository.Implementations;

namespace ApplicationService.Implementations
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryManagementService(IUnitOfWork _unitOfWork) => unitOfWork = _unitOfWork;

        public async Task<IEnumerable<CategoryDTO>> Get(string query)
        {
            List<CategoryDTO> categoriesDto = new();

            if (string.IsNullOrWhiteSpace(query))
            {
                using (unitOfWork)
                {
                    var categories = await unitOfWork.CategoryRepository.GetCategoriesAsync();

                    categoriesDto = ObjectMapper.Mapper.Map<List<CategoryDTO>>(categories);
                }
            }

            return categoriesDto;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            using (unitOfWork)
            {
                Category category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

                if (category is null)
                {
                    throw new NotFoundException(id, nameof(Category));
                }

                categoryDTO = ObjectMapper.Mapper.Map<CategoryDTO>(category);
            }

            return categoryDTO;
        }

        public async Task<CategoryDTO> Save(CategoryDTO categoryDTO)
        {
            Category category = ObjectMapper.Mapper.Map<Category>(categoryDTO);

            if (categoryDTO.Id == 0)
            {
                unitOfWork.CategoryRepository.Create(category);
            }
            else
            {
                unitOfWork.CategoryRepository.Update(category);
            }

            await unitOfWork.SaveChangesAsync();

            var categoryToReturn = ObjectMapper.Mapper.Map<CategoryDTO>(category); 

            return categoryToReturn;
        }



        public async Task Delete(int id)
        {
            Category category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException(id, nameof(Category));
            }

            unitOfWork.CategoryRepository.Delete(category);

            await unitOfWork.SaveChangesAsync();
        }
    }
}

