using ApplicationService.DTOs;

namespace ApplicationService.Contracts
{
    public interface ICategoryManagementService
    {
        Task<IEnumerable<CategoryDTO>> Get(string query);
        Task<CategoryDTO> GetById(int id);
        Task<CategoryDTO> Save(CategoryDTO categoryDTO);
        Task<CategoryDTO> Update(CategoryDTO categoryDTO);
        Task Delete(int id);
    }
}
