using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.DTOs.SellerDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellerController : ControllerBase
{
    private readonly ISellerManagementService sellerService;
    private readonly ILogger<CategoryController> logger;

    public SellerController(ISellerManagementService sellerService, ILogger<CategoryController> logger)
    {
        this.sellerService = sellerService;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SellerDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Route("[action]")]
    public async Task<IActionResult> Get(string query)
    {
        var result = await sellerService.GetAsync(query);

        if (result.Any())
        {
            logger.Log(LogLevel.Information, "Succesfully getting list of sellers");
            return Ok(result);
        }
        else
        {
            logger.Log(LogLevel.Error, "Cannot load a sellers");
            return NoContent();
        }
    }

    [HttpGet("[action]/{id:int}", Name = "SellerById")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await sellerService.GetByIdAsync(id);

        logger.Log(LogLevel.Information, $"Succesfully getting a seller with id {result.Id}");
        return Ok(result);
    }

    [Authorize("Admin")]
    [Route("[action]")]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Save([FromForm]SellerDTO seller)
    {
        SellerDTO sellerToReturn = await sellerService.SaveAsync(seller);

        return CreatedAtRoute("SellerById", new { id = sellerToReturn.Id }, sellerToReturn);
    }

    [Authorize("Admin")]
    [Route("[action]")]
    [HttpPut]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Update([FromBody] SellerDTO seller)
    {
        SellerDTO sellerToReturn = await sellerService.UpdateAsync(seller);

        return CreatedAtRoute("SellerById", new { id = sellerToReturn.Id }, sellerToReturn);
    }

    [Authorize("Admin, Market")]
    [Route("[action]/{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await sellerService.DeleteAsync(id);

        logger.Log(LogLevel.Information, $"Seller with id {id} was deleted");

        return Ok($"Seller with id {id} was deleted");
    }
}
