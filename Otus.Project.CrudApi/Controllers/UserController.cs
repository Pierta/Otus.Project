using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.CrudApi.Services;

namespace Otus.Project.CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
