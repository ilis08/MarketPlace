using ApplicationService.DTOs;
using ApplicationService.DTOs.SellerDTOs;
using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Contracts;

public interface ISellerManagementService
{
    Task<IEnumerable<SellerDTO>> GetAsync(string query);
    Task<SellerDTO> GetByIdAsync(long id);
    Task<SellerDTO> SaveAsync(SellerDTO seller);
    Task<SellerDTO> UpdateAsync(SellerDTO seller);
    Task DeleteAsync(long id);
}
