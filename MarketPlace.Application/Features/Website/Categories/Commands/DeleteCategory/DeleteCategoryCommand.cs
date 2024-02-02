using MediatR;

namespace MarketPlace.Application.Features.Website.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public long Id { get; set; }
}
