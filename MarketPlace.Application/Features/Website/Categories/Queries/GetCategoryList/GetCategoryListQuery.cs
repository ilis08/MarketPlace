using MediatR;

namespace MarketPlace.Application.Features.Website.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : IRequest<List<CategoryListVm>>
{
}
