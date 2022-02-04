using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public JsonResult Version()
        {
            return Json("REST-API e-Store. Version 1.0");
        }
    }
}
