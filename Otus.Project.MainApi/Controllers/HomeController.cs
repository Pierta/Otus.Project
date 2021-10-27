using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Otus.Project.MainApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{param}")]
        public IActionResult Get([FromRoute] string param)
        {
            _logger.LogInformation($"GET '/Home' with param = '{param}' has been requested");
            
            return Ok($"{param}");
        }
    }
}
