using MarketPlace.Application.Responses;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.CreateProduct;

public class CreateProductCommandResponse : BaseResponse
{
    public CreateProductCommandResponse() : base()
    {

    }

    public CreateProductDto Product { get; set; } = default!;
}
