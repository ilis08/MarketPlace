using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ResponseMessage responseMessage;

        public CategoryController(CategoryManagementService _service, ILogger<CategoryController> _logger, ResponseMessage _message)
        {
            service = _service;
            logger = _logger;
            responseMessage = _message;
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

        [HttpGet("[action]/{id:int}")]
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
        public async Task<IActionResult> Save([FromBody]CategoryDTO categoryDto)
        {
            if (categoryDto.Title == null && categoryDto.Description == null)
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            if (await service.Save(categoryDto))
            {
                logger.Log(LogLevel.Information, "Category was saved");
                responseMessage.Code = 201;
                responseMessage.Body = "Category was saved";
            }
            else
            {
                logger.Log(LogLevel.Error, "Category was not saved");
                responseMessage.Code = 202;
                responseMessage.Body = "Category was not saved";
            }

            return Json(responseMessage);
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var body = await service.Delete(id);

            if (body)
            {
                logger.LogInformation("Category was deleted");
                responseMessage.Code = 200;
                responseMessage.Body = "Category was deleted";
                return Ok();
            }
            else
            {
                logger.LogError("Category was not deleted");
                responseMessage.Body = 304;
                responseMessage.Body = "Category was not deleted";
                return Json("Category was not deleted");
            }
        }

      
    }
}
