using MarketPlace.Application.Features.Categories.Commands.CreateCategory;
using MarketPlace.Application.Features.Categories.Commands.DeleteCategory;
using MarketPlace.Application.Features.Categories.Commands.UpdateCategory;
using MarketPlace.Application.Features.Categories.Queries.GetCategoryList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Controller to work with Categories
/// </summary>
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : HomeController
{
    private readonly IMediator mediator;

    public CategoryController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("[action]")]
    public async Task<ActionResult<List<CategoryListVm>>> Get()
    {
        var dtos = await mediator.Send(new GetCategoryListQuery());

        return Ok(dtos);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromForm] CreateCategoryCommand createCategoryCommand)
    {
        var response = await mediator.Send(createCategoryCommand);

        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
    {
        await mediator.Send(updateCategoryCommand);

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleteEventCommand = new DeleteCategoryCommand() { Id = id };
        await mediator.Send(deleteEventCommand);

        return NoContent();
    }
}
