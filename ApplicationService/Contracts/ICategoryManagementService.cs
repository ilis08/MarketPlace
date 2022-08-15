using ApplicationService.DTOs;

namespace ApplicationService.Contracts
{
    public interface ICategoryManagementService
    {
        Task<IEnumerable<CategoryDTO>> GetAsync(string query);
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<CategoryDTO> SaveAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDTO);
        Task DeleteAsync(int id);
    }
}
