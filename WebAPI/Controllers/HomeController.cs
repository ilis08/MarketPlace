using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Version()
    {
        return Ok("REST-API e-Store. Version 1.0");
    }
}
