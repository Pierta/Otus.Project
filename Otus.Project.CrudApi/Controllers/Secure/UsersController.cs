using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.CrudApi.Model;
using Otus.Project.CrudApi.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.CrudApi.Secure.Controllers
{
    [Authorize]
    [ApiController]
    [Route("secure/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(ILogger<UsersController> logger,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> Get(Guid userId, CancellationToken ct)
        {
            _logger.LogInformation("'Get User' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            if (userIdFromToken != userId)
            {
                return Forbid();
            }

            try
            {
                var user = await _userService.GetUser(userId, ct);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{userId:Guid}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserModel newUser, CancellationToken ct)
        {
            _logger.LogInformation("'Update User' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            if (userIdFromToken != userId)
            {
                return Forbid();
            }

            try
            {
                await _userService.UpdateUser(userId, newUser, ct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok($"User {newUser.FirstName} {newUser.LastName} has been updated!");
        }
    }
}
