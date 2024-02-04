using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository productRepository;
    private readonly IImageService imageService;

    public DeleteProductHandler(
        IProductRepository productRepository,
        IImageService imageService)
    {
        this.productRepository = productRepository;
        this.imageService = imageService;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productToDelete = await productRepository.FindByIdAsync(request.Id);

        if (productToDelete == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        await productRepository.DeleteAsync(productToDelete);
    }
}
