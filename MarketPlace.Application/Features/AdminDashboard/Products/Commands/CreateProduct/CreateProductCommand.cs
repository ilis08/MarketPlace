using MediatR;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductCommandResponse>
{
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly? ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public IFormFile LogoImage { get; set; } = default!;
    public IEnumerable<IFormFile> Images { get; set; } = default!;
    public long CategoryId { get; set; }
    public long SellerId { get; set; }
}
