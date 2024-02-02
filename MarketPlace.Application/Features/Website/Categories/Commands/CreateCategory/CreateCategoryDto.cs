namespace MarketPlace.Application.Features.Website.Categories.Commands.CreateCategory;

public class CreateCategoryDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}
