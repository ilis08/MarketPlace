using FilesService;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGreetMessage(string name)
        {
            var channel = GrpcChannel.ForAddress(new Uri("https://localhost:7035"));
            var client = new Greeter.GreeterClient(channel);

            using var result = client.SendMessage();

            List<string> number = new List<string>();

            try
            {
                await foreach (var item in result.ResponseStream.ReadAllAsync(CancellationToken.None))
                {
                    number.Add(item.Text);
                }
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }

            return Ok(number);
        }
    }
}
