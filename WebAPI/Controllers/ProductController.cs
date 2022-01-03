using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Implementations;
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
        private readonly ProductManagementService service = null;
        public static IWebHostEnvironment _environment;
        private readonly ILogger<ProductController> logger;

        public ProductController(IWebHostEnvironment environment, ProductManagementService _service, ILogger<ProductController> _logger)
        {
            _environment = environment;
            service = _service;
            logger = _logger;   
        }

        /// <summary>
        /// Returns all Categories if query==null, or if query!=null return Categories which contains 'query' 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(string query)
        {
            var body = await service.Get(query);

            if (body.Any())
            {
                logger.Log(LogLevel.Information, "Succesfully getting list of products");
                return Ok(body);
            }
            else
            {
                logger.Log(LogLevel.Error, "Cannot load a products");
                return NoContent();
            }

        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await service.GetById(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsByParams([FromQuery] GetProductsParameters productsParameters)
        {
            return Json(await service.GetProductsByParameters(productsParameters));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Save([FromForm]ProductDTO productDto)
        {
            if (productDto.ProductName == null)
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            ResponseMessage responseMessage = new();

            if (await service.Save(productDto))
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

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductDTO product)
        {
            if (product.ProductName == default(string))
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            ResponseMessage responseMessage = new();

            if (await service.Update(product))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Product was updated";
            }
            else
            {
                responseMessage.Code = 202;
                responseMessage.Body = "Product was not updated";
            }

            return Json(responseMessage);
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await service.Delete(id));
        }
    }
}
