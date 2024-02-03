using MediatR;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public long ParentCategoryId { get; set; } = default!;
    public IFormFile? Image { get; set; }
}
