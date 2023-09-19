using ApplicationService.Contracts;
using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers;

[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class OrderController : HomeController
{
    private readonly IOrderManagementService orderService;
    private readonly ILogger<OrderController> logger;

    public OrderController(IOrderManagementService _service, ILogger<OrderController> _logger)
    {
        orderService = _service;
        logger = _logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Route("[action]")]
    public async Task<IActionResult> Get()
    {
        var result = await orderService.GetAsync();

        if (result.Any())
        {
            logger.Log(LogLevel.Information, "Succesfully getting list of orders");
            return Ok(result);
        }
        else
        {
            logger.Log(LogLevel.Error, "Cannot load a orders");
            return NoContent();
        }
    }

    [HttpGet("[action]/{id:long}", Name = "OrderById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await orderService.GetByIdAsync(id);

        logger.Log(LogLevel.Information, "Succesfully getting a order");
        return Ok(result);
    }

    [Route("[action]")]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Save([FromBody] OrderDTO orderDTO)
    {
        var orderToReturn = await orderService.SaveAsync(orderDTO);

        return CreatedAtRoute("OrderById", new { id = orderToReturn.Id }, orderToReturn);
    }

    [Route("[action]")]
    [HttpPut]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Update([FromBody] OrderDTO orderDTO)
    {
        var result = await orderService.UpdateAsync(orderDTO);

        return CreatedAtRoute("OrderById", new { id = result.Id }, result);
    }

    [Route("[action]/{id:int}")]
    [HttpPut]
    public async Task<IActionResult> CompleteOrder(int id)
    {
        await orderService.CompleteOrderAsync(id);

        return Ok("Order was completed succesfully");
    }

    [Route("[action]/{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await orderService.DeleteAsync(id);

        return Ok("Order was deleted succesfully");
    }
}
