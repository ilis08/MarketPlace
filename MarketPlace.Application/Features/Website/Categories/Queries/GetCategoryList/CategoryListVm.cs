namespace MarketPlace.Application.Features.Website.Categories.Queries.GetCategoryList;

public class CategoryListVm
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Image { get; set; } = default!;
}
