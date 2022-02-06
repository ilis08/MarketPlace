using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;
using Repository.Implementations;
using Repository.RequestFeatures;
using System.Text.Json;
using WebAPI.Filters;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : HomeController
    {
        private readonly ProductManagementService service = null;
        private readonly ILogger<ProductController> logger;

        public ProductController(ProductManagementService _service, ILogger<ProductController> _logger)
        {
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
            var product = await service.GetById(id);

            logger.Log(LogLevel.Information, "Succesfully getting a product");

            return Ok(product);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsByParams([FromQuery]ProductParameters productsParameters)
        {
            var pagedResult = await service.GetProductsByParameters(productsParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            logger.Log(LogLevel.Information, "Succesfully list of a products by parameters");

            return Ok(pagedResult.products);
        }

        [Route("[action]")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Save([FromForm]ProductDTO product, [FromServices] ResponseMessage responseMessage)
        {
            if (await service.Save(product))
            {
                logger.Log(LogLevel.Information, "Product was saved");
                responseMessage.Code = 201;
                responseMessage.Body = "Product was saved";
            }
            else
            {
                logger.Log(LogLevel.Error, "Product was not saved");
                responseMessage.Code = 202;
                responseMessage.Body = "Product was not saved";
            }

            return Json(responseMessage);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductDTO product, [FromServices] ResponseMessage responseMessage)
        {
            if (product.ProductName == default(string))
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            if (await service.Update(product))
            {
                logger.Log(LogLevel.Information, "Product was updated");
                responseMessage.Code = 201;
                responseMessage.Body = "Product was updated";
            }
            else
            {
                logger.Log(LogLevel.Error, "Product was not updated");
                responseMessage.Code = 202;
                responseMessage.Body = "Product was not updated";
            }

            return Json(responseMessage);
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await service.Delete(id);

            logger.Log(LogLevel.Information, "Product was deleted succesfully");

            return Ok("Product was deleted succesfully");
        }
    }
}
