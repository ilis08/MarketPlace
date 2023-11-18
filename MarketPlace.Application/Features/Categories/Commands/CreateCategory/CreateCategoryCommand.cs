using MediatR;

namespace MarketPlace.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Image { get; set; } = default!;
}
