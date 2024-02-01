using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IAsyncRepository<Category> categoryRepository;

    public DeleteCategoryHandler(IAsyncRepository<Category> _categoryRepository)
    {
        categoryRepository = _categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToDelete = await categoryRepository.FindByIdAsync(request.Id);

        if (categoryToDelete == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        await categoryRepository.DeleteAsync(categoryToDelete);
    }
}
