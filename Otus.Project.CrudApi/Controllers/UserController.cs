using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.CrudApi.Services;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogInformation("GetUsers action has been requested");
            var users = await _userService.GetUsers(ct);

            return Ok(users);
        }
    }
}
