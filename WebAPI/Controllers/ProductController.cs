using ApplicationService.Contracts;
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
        private readonly IProductManagementService service;
        private readonly ILogger<ProductController> logger;

        public ProductController(IProductManagementService _service, ILogger<ProductController> _logger)
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
                logger.Log(LogLevel.Information, "Succesfully getting a list of products");
                return Ok(body);
            }
            else
            {
                logger.Log(LogLevel.Error, "Cannot load a products");
                return NoContent();
            }

        }

        [HttpGet("[action]/{id:int}", Name = "ProductById")]
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

            logger.Log(LogLevel.Information, "Succesfully getting a list of a products by parameters");

            return Ok(pagedResult.products);
        }

        [Route("[action]")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Save([FromForm]ProductDTO product)
        {
            var productToReturn = await service.Save(product);

            return CreatedAtRoute("ProductById", new { id = productToReturn.Id }, productToReturn);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductDTO product)
        {
            var productToReturn = await service.Update(product);

            return CreatedAtRoute("ProductById", new { id = productToReturn.Id }, productToReturn);
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
