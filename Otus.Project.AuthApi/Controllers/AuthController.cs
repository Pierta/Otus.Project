using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.AuthApi.Model;
using Otus.Project.AuthApi.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model, CancellationToken ct)
        {
            var response = await _userService.Authenticate(model, ct);
            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Check()
        {
            _logger.LogInformation("'Check auth' action has been requested. Token is valid!");
            return Ok("Token is valid!");
        }
    }
}
