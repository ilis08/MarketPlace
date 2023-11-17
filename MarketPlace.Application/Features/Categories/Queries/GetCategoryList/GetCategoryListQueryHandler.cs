using AutoMapper;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.Features.Categories.Queries.GetCategoryList;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery,
    List<CategoryListVm>>
{
    private readonly ILogger<GetCategoryListQueryHandler> logger;
    private readonly IAsyncRepository<Category> categoryRepository;
    private readonly IMapper mapper;

    public GetCategoryListQueryHandler(IAsyncRepository<Category> categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<List<CategoryListVm>> Handle(GetCategoryListQuery request,
        CancellationToken cancellationToken)
    {
        var allCategories = (await categoryRepository.FindAllAsync()).OrderBy(x => x.Title);

        return mapper.Map<List<CategoryListVm>>(allCategories);
    }
}
