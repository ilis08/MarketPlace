using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.DTOs.SellerDTOs;
using ApplicationService.Mapper;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations;

public class SellerManagementService : ISellerManagementService
{
    private readonly IRepository repository;
    public SellerManagementService(IRepository _repository) => repository = _repository;

    public async Task<IEnumerable<SellerDTO>> GetAsync(string query)
    {
        IQueryable<Seller> sellersQuery = repository.FindAll<Seller>();

        if (string.IsNullOrWhiteSpace(query))
        {
            sellersQuery = sellersQuery.Where(x => x.Name.Contains(query));
        }

        var sellers = await sellersQuery.ToListAsync();

        return ObjectMapper.Mapper.Map<List<SellerDTO>>(sellers);
    }

    public async Task<SellerDTO> GetByIdAsync(long id)
    {
        Seller seller = await repository.FindByIdAsync<Seller>(id);

        if (seller is null)
        {
            throw new NotFoundException(id, nameof(Seller));
        }

        return ObjectMapper.Mapper.Map<SellerDTO>(seller);
    }
    public async Task<SellerDTO> SaveAsync(SellerDTO sellerDTO)
    {
        Seller seller = ObjectMapper.Mapper.Map<Seller>(sellerDTO);

        var user = await repository.FindByIdAsync<ApplicationUser>(seller.UserId);

        if (user is null)
        {
            throw new Exception($"User with id : {seller.UserId} not found when SaveAsync() seller in SellerManagementService");
        }

        await repository.CreateAsync(seller);

        await repository.SaveChangesAsync();

        var sellerToReturn = ObjectMapper.Mapper.Map<SellerDTO>(seller);

        return sellerToReturn;
    }

    public async Task<SellerDTO> UpdateAsync(SellerDTO sellerDTO)
    {
        Seller seller = ObjectMapper.Mapper.Map<Seller>(sellerDTO);

        var user = await repository.FindByIdAsync<ApplicationUser>(seller.UserId);

        if (user is null)
        {
            throw new Exception($"User with id : {seller.UserId} not found when UpdateAsync() seller in SellerManagementService");
        }

        repository.Update(seller);

        await repository.SaveChangesAsync();

        var sellerToReturn = ObjectMapper.Mapper.Map<SellerDTO>(seller);

        return sellerToReturn;
    }

    public async Task DeleteAsync(long id)
    {
        Seller seller = await repository.FindByIdAsync<Seller>(id);

        if (seller is null)
        {
            throw new NotFoundException(id, nameof(Seller));
        }

        repository.Delete(seller);

        await repository.SaveChangesAsync();
    }
}
