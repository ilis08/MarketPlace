using MediatR;

namespace MarketPlace.Application.Features.Website.Products.Queries.GetProductList;

public class GetProductListQuery : IRequest<List<ProductListVm>>
{
}
