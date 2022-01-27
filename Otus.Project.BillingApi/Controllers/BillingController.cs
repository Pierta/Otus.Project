using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.BillingApi.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.BillingApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBillingAccountService _billingAccountService;

        public BillingController(ILogger<BillingController> logger,
            IHttpContextAccessor httpContextAccessor,
            IBillingAccountService billingAccountService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _billingAccountService = billingAccountService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CancellationToken ct)
        {
            _logger.LogInformation("'Add billing account' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var user = await _billingAccountService.CreateNewBillingAccountIfNotExist(userIdFromToken.Value, ct);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogInformation("'Get current balance' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var user = await _billingAccountService.GetCurrentBalance(userIdFromToken.Value, ct);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("topUp/{value:decimal}")]
        public async Task<IActionResult> TopUp(decimal value, CancellationToken ct)
        {
            _logger.LogInformation("'Top up balance' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var currentBalance = await _billingAccountService.TopUpBalance(userIdFromToken.Value, value, ct);
                return Ok($"Balance has been topped up (+{value}). Current balance: {currentBalance}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("withdraw/{value:decimal}")]
        public async Task<IActionResult> Withdraw(decimal value, CancellationToken ct)
        {
            _logger.LogInformation("'Withdraw money' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var currentBalance = await _billingAccountService.WithdrawMoney(userIdFromToken.Value, value, ct);
                return Ok($"The money ({value}) was debited from the account. Current balance: {currentBalance}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
