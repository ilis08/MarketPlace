using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : HomeController
    {
        private readonly OrderManagementService _service;
        private readonly ILogger<OrderController> _logger;

        public OrderController(OrderManagementService service, ILogger<OrderController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            return Json(await _service.Get());
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await _service.GetById(id));
        }

        [Route("[action]")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Save([FromBody] OrderDTO orderDTO)
        { 
            ResponseMessage responseMessage = new ResponseMessage();

            if (await _service.Save(orderDTO))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Order was saved";
            }
            else
            {
                responseMessage.Code = 202;
                responseMessage.Body = "Order was not saved";
            }

            return Json(responseMessage);
        }

        [Route("[action]")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Update([FromBody] OrderDTO orderDTO, [FromServices]ResponseMessage responseMessage)
        {
            if (await _service.Update(orderDTO))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Order was updated";
            }
            else
            {
                responseMessage.Code = 202;
                responseMessage.Body = "Order was not updated";
            }

            return Json(responseMessage);
        }

        [Route("[action]/{id:int}")]
        [HttpPut]
        public async Task<JsonResult> CompleteOrder(int id, [FromServices] ResponseMessage responseMessage)
        {
            if (id != 0)
            {
                if (await _service.CompleteOrderAsync(id))
                {
                    responseMessage.Code = 201;
                    responseMessage.Body = "Order was completed";
                }
                else
                {
                    responseMessage.Code = 202;
                    responseMessage.Body = "Order was not completed";
                }
            }
            return Json(responseMessage);
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return Ok("Order was deleted succesfully");
        }
    }
}
