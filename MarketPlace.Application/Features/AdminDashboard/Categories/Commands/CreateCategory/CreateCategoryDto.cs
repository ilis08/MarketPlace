namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.CreateCategory;

public class CreateCategoryDto
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
