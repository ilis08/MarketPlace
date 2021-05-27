using ApplicationService.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManagementService _service = null;

        public CategoryController()
        {
            _service = new CategoryManagementService();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return JsonResult(_service.Get());
        }
    }
}
