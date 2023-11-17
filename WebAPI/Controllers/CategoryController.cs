using MarketPlace.Application.Features.Products.Queries.GetProductList;
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

    /// <summary>
    /// Returns all Categories if query==null, or if query!=null return Categories which contains 'query' 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("[action]")]
    public async Task<IActionResult> Get(string query)
    {
        var dtos = await mediator.Send(new GetProductListQuery());

        return Ok(dtos);
    }


    /*

[HttpGet("[action]/{id:int}", Name = "CategoryById")]
public async Task<IActionResult> GetById(int id)
{

}

    [Authorize("Admin, Market")]
    [Route("[action]")]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Save([FromForm]CategoryDTO category)
    {

    }

    [Authorize("Admin, Market")]
    [Route("[action]")]
    [HttpPut]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Update([FromBody] CategoryDTO category)
    {



    [Authorize("Admin, Market")]
    [Route("[action]/{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {

    }
        }*/
}
