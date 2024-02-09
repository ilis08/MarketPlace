using AutoMapper;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.Features.Website.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery,
    List<ProductListVm>>
{
    private readonly ILogger<GetProductListQueryHandler> logger;
    private readonly IAsyncRepository<Product> productRepository;
    private readonly IMapper mapper;

    public GetProductListQueryHandler(
        IAsyncRepository<Product> productRepository,
        IMapper mapper,
        ILogger<GetProductListQueryHandler> logger)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<List<ProductListVm>> Handle(GetProductListQuery request,
        CancellationToken cancellationToken)
    {
        var allProducts = (await productRepository.FindAllAsync()).OrderBy(x => x.ReleaseDate);

        return mapper.Map<List<ProductListVm>>(allProducts);
    }
}
