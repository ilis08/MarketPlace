using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly CategoryManagementService _service = null;

        public CategoryController()
        {
            _service = new CategoryManagementService();
        }


        /// <summary>
        /// Returns all Categories if query==null, or if query!=null return Categories which contains 'query' 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public JsonResult Get(string query)
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
        public JsonResult Save([FromBody]CategoryDTO categoryDto)
        {
            if (categoryDto.Title == null && categoryDto.Description == null)
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Save(categoryDto))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Category was saved";
            }
            else
            {
                responseMessage.Code = 202;
                responseMessage.Body = "Category was not saved";
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
