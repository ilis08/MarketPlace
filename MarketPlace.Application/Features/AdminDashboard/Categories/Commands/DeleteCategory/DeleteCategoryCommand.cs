using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public long Id { get; set; }
    public Guid ImageId { get; set; }
}
