using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Messages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : HomeController
    {
        private readonly OrderManagementService _service;
        private readonly ResponseMessage responseMessage;
        public static IWebHostEnvironment _environment;

        public OrderController(IWebHostEnvironment environment, OrderManagementService service, ResponseMessage message)
        {
            _service = service;
            _environment = environment;
            this.responseMessage = message;
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
        public JsonResult Save([FromBody] OrderDTO orderDTO)
        { 

            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Save(orderDTO))
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
        public JsonResult Update([FromBody] OrderDTO orderDTO)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Update(orderDTO))
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
        [HttpPost]
        public async Task<JsonResult> CompleteOrder(int id)
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
        public JsonResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
