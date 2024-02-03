using AutoMapper;
using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Contracts.Infrastructure.Validations;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand,
CreateCategoryCommandResponse>
{
    private readonly IAsyncRepository<Category> categoryRepository;
    private readonly IImageService imageService;
    private readonly IImageValidation imageValidation;
    private readonly IMapper mapper;

    public CreateCategoryCommandHandler(
        IMapper mapper,
        IImageService imageService,
        IAsyncRepository<Category> categoryRepository,
        IImageValidation imageValidation)
    {
        this.mapper = mapper;
        this.imageService = imageService;
        this.imageValidation = imageValidation;
        this.categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var createCategoryCommandResponse = new CreateCategoryCommandResponse();

        var validator = new CreateCategoryCommandValidator(imageValidation);
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
            var imageUrl = await imageService.UploadImage(request.Image);

            var category = new Category()
            {
                Title = request.Title,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId,
                Image = new Image
                {
                    Url = imageUrl,
                }
            };

            category = await categoryRepository.AddAsync(category);

            createCategoryCommandResponse.Category = mapper.Map<CreateCategoryDto>(category);
        }

        return createCategoryCommandResponse;
    }
}

