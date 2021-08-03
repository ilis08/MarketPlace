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
        public static IWebHostEnvironment _environment;

        public OrderController(IWebHostEnvironment environment, OrderManagementService service)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get()
        {
            return Json(_service.Get());
        }

        [HttpGet("[action]/{id:int}")]
        public IActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
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

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
