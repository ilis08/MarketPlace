namespace MarketPlace.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Image { get; set; } = default!;
    }
}
