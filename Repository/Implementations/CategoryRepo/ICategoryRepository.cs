using Data.Entitites;

namespace Repository.Implementations.CategoryRepo
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        void Create(Category entity);

        void Update(Category entity);

        void Delete(Category entity);
    }
}
