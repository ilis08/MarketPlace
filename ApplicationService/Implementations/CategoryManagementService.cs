using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.BaseRepo;

namespace ApplicationService.Implementations
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly IRepository repository;

        public CategoryManagementService(IRepository _repository) => repository = _repository;

        public async Task<IEnumerable<CategoryDTO>> Get(string query)
        {
            List<CategoryDTO> categoriesDto = new();

            if (string.IsNullOrWhiteSpace(query))
            {
                using (repository)
                {
                    var categories = await repository.FindAll<Category>().ToListAsync();

                    categoriesDto = ObjectMapper.Mapper.Map<List<CategoryDTO>>(categories);
                }
            }

            return categoriesDto;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            using (repository)
            {
                Category category = await repository.FindByCondition<Category>(x => x.Id == id).FirstOrDefaultAsync();

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
                await repository.CreateAsync(category);
            }
            else
            {
                repository.Update(category);
            }

            await repository.SaveChangesAsync();

            var categoryToReturn = ObjectMapper.Mapper.Map<CategoryDTO>(category); 

            return categoryToReturn;
        }



        public async Task Delete(int id)
        {
            Category category = await repository.FindByCondition<Category>(x => x.Id == id)
                                                .FirstOrDefaultAsync();

            if (category is null)
            {
                throw new NotFoundException(id, nameof(Category));
            }

            repository.Delete(category);

            await repository.SaveChangesAsync();
        }
    }
}

