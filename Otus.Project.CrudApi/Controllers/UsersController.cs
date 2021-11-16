using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.CrudApi.Model;
using Otus.Project.CrudApi.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogInformation("'Get Users' action has been requested");
            var users = await _userService.GetUsers(ct);

            return Ok(users);
        }

        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> Get(Guid userId, CancellationToken ct)
        {
            _logger.LogInformation("'Get User' action has been requested");
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel newUser, CancellationToken ct)
        {
            _logger.LogInformation("'Add User' action has been requested");
            var newUserId = await _userService.AddUser(newUser, ct);

            return Ok(newUserId);
        }

        [HttpPut("{userId:Guid}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserModel newUser, CancellationToken ct)
        {
            _logger.LogInformation("'Update User' action has been requested");
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

        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> Delete(Guid userId, CancellationToken ct)
        {
            _logger.LogInformation("'Delete User' action has been requested");
            try
            {
                await _userService.DeleteUser(userId, ct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok($"User with id = '{userId}' has been deleted!");
        }
    }
}
