using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public long Id { get; set; }
}
