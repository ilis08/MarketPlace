using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IAsyncRepository<Category> categoryRepository;
    private readonly IImageService imageService;

    public DeleteCategoryHandler(
        IAsyncRepository<Category> categoryRepository,
        IImageService imageService)
    {
        this.categoryRepository = categoryRepository;
        this.imageService = imageService;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await imageService.DeleteImage(request.ImageId);

        var categoryToDelete = await categoryRepository.FindByIdAsync(request.Id);

        if (categoryToDelete == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        await categoryRepository.DeleteAsync(categoryToDelete);
    }
}
