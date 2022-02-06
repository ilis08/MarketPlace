using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to work with Categories
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : HomeController
    {
        private readonly CategoryManagementService service;
        private readonly ILogger<CategoryController> logger;

        public CategoryController(CategoryManagementService _service, ILogger<CategoryController> _logger)
        {
            service = _service;
            logger = _logger;
        }


        /// <summary>
        /// Returns all Categories if query==null, or if query!=null return Categories which contains 'query' 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(string query)
        {
            var body = await service.Get(query);

            if (body.Any())
            {
                logger.Log(LogLevel.Information, "Succesfully getting list of categories");
                return Ok(body);
            }
            else
            {
                logger.Log(LogLevel.Error, "Cannot load a categories");
                return NoContent();
            }

        }

        [HttpGet("[action]/{id:int}", Name = "CategoryById")]
        public async Task<IActionResult> GetById(int id)
        {
            var body = await service.GetById(id);

            if (body.Id > 0)
            {
                logger.Log(LogLevel.Information, "Succesfully getting a category");
                return Ok(body);
            }
            else
            {
                logger.Log(LogLevel.Error, "Cannot load a specified category");
                return NoContent();
            }
        }

        [Route("[action]")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Save([FromBody]CategoryDTO category)
        {
            var categoryToReturn = await service.Save(category);

            return CreatedAtRoute("CategoryById", new { id = categoryToReturn.Id }, categoryToReturn);
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromServices] ResponseMessage responseMessage)
        {
            await service.Delete(id);

            logger.Log(LogLevel.Information,"Category was deleted");

            return Ok("Category was deleted succesfully");
        }

      
    }
}
