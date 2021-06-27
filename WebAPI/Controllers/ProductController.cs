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

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : HomeController
    {
        private readonly ProductManagementService _service = null;
        public static IWebHostEnvironment _environment;

        public ProductController(IWebHostEnvironment environment)
        {
            _service = new ProductManagementService();
            _environment = environment;
        }

        /// <summary>
        /// Returns all Categories if query==null, or if query!=null return Categories which contains 'query' 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        
        [HttpGet]
        [Route("[action]")]
        public IActionResult Get(string query)
        {
            return Json(_service.Get(query));
        }

        [HttpGet("[action]/{id:int}")]
        public JsonResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        [Route("[action]")]
        [HttpPost]
        public JsonResult Save([FromForm]ProductDTO productDto)
        {
            if (productDto.ProductName == null)
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Save(productDto))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Product was saved";
            }
            else
            {
                responseMessage.Code = 202;
                responseMessage.Body = "Product was not saved";
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
