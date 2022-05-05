using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
