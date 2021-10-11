using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entitites;

namespace Repository.Implementations.CategoryRepo
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category> GetCategorytByIdAsync(int id);

        void Create(Category entity);

        void Update(Category entity);

        void Delete(Category entity);
    }
}
