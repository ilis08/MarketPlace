using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace ApplicationService.Implementations;

public class CategoryManagementService : ICategoryManagementService
{
    private readonly IRepository repository;

    public CategoryManagementService(IRepository _repository) => repository = _repository;

    public async Task<IEnumerable<CategoryDTO>> GetAsync(string query = "")
    {
        IQueryable<Category> categoriesQuery = repository.FindAll<Category>();

        if (string.IsNullOrWhiteSpace(query))
        {
            categoriesQuery = categoriesQuery.Where(x => x.Title.Contains(query));
        }

        var categories = await categoriesQuery.ToListAsync();

        return ObjectMapper.Mapper.Map<List<CategoryDTO>>(categories);
    }

    public async Task<CategoryDTO> GetByIdAsync(long id)
    {
        Category category = await repository.FindByIdAsync<Category>(id);

        if (category is null)
        {
            throw new NotFoundException(id, nameof(Category));
        }

        return ObjectMapper.Mapper.Map<CategoryDTO>(category);
    }

    public async Task<CategoryDTO> SaveAsync(CategoryDTO categoryDTO)
    {
        Category category = ObjectMapper.Mapper.Map<Category>(categoryDTO);

        await repository.CreateAsync(category);

        await repository.SaveChangesAsync();

        var categoryToReturn = ObjectMapper.Mapper.Map<CategoryDTO>(category);

        return categoryToReturn;
    }

    public async Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDTO)
    {
        var category = ObjectMapper.Mapper.Map<Category>(categoryDTO);

        repository.Update(category);

        await repository.SaveChangesAsync();

        var categoryToReturn = ObjectMapper.Mapper.Map<CategoryDTO>(category);

        return categoryToReturn;
    }

    public async Task DeleteAsync(long id)
    {
        Category category = await repository.FindByIdAsync<Category>(id);

        if (category is null)
        {
            throw new NotFoundException(id, nameof(Category));
        }

        repository.Delete(category);

        await repository.SaveChangesAsync();
    }
}

