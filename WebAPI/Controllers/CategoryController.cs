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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : HomeController
    {
        private readonly CategoryManagementService _service = null;

        public CategoryController()
        {
            _service = new CategoryManagementService();
        }

        [HttpGet]
        [Route("[action]")]
        public JsonResult Get(string query)
        {
            return Json(_service.Get(query));
        }

        [HttpGet]
        [Route("[action]/id")]
        public JsonResult GetById(long id)
        {
            return Json(_service.GetById(id));
        }

        [HttpGet]
        [Route("[action]")]
        public JsonResult Save(CategoryDTO categoryDto)
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
    }
}
