using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Version()
    {
        return Ok("Market Place API. Version 1.0");
    }
}
