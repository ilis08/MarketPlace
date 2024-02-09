namespace MarketPlace.Application.Features.Website.Products.Queries.GetProductList;

public class ProductListVm
{
    public long Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly? ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public string LogoImageUrl { get; set; } = default!;
    public long CategoryId { get; set; }
    public long SellerId { get; set; }
}
