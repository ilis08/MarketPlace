using AutoMapper;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.Website.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand,
CreateCategoryCommandResponse>
{
    private readonly IAsyncRepository<Category> categoryRepository;
    private readonly IMapper mapper;

    public CreateCategoryCommandHandler(IMapper _mapper, IAsyncRepository<Category> _categoryRepository)
    {
        mapper = _mapper;
        categoryRepository = _categoryRepository;
    }

    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var createCategoryCommandResponse = new CreateCategoryCommandResponse();

        var validator = new CreateCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createCategoryCommandResponse.Success = false;
            createCategoryCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        if (createCategoryCommandResponse.Success)
        {
            var category = new Category()
            {
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
            };
            category = await categoryRepository.AddAsync(category);

            createCategoryCommandResponse.Category = mapper.Map<CreateCategoryDto>(category);
        }

        return createCategoryCommandResponse;
    }
}

