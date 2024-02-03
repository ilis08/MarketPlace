using MediatR;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
    public long? ParentCategoryId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public IFormFile Image { get; set; } = default!;
}
