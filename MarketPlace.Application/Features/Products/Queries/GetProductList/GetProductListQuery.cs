using MediatR;

namespace MarketPlace.Application.Features.Products.Queries.GetProductList;

public class GetProductListQuery : IRequest<List<ProductListVm>>
{
}
