using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Application.Features.Website.Products.Queries.GetProductList;

public class ProductListVm
{
    public long Id { get; set; }
    [Required]
    public string ProductName { get; set; } = default!;
    [Required]
    public string Description { get; set; } = default!;
    [Required]
    public DateTime? Release { get; set; }
    [Required]
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public long SellerId { get; set; }
}
