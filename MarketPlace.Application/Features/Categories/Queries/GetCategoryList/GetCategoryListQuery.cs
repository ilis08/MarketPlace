using MediatR;

namespace MarketPlace.Application.Features.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : IRequest<List<CategoryListVm>>
{
}
