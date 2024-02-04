namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.CreateProduct;

public class CreateProductDto
{
    public long Id { get; set; }
    public string ProductName { get; set; } = default!;
}
