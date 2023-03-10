using ApplicationService.DTOs;

namespace ApplicationService.Contracts
{
    public interface ICategoryManagementService
    {
        Task<IEnumerable<CategoryDTO>> GetAsync(string query);
        Task<CategoryDTO> GetByIdAsync(long id);
        Task<CategoryDTO> SaveAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDTO);
        Task DeleteAsync(long id);
    }
}
