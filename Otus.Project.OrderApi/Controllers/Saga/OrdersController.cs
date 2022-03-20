using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.OrderApi.Model;
using Otus.Project.OrderApi.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Controllers.Saga
{
    [Authorize]
    [ApiController]
    [Route("Saga/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISagaOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger,
            IHttpContextAccessor httpContextAccessor,
            ISagaOrderService orderService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderModel orderModel, CancellationToken ct)
        {
            _logger.LogInformation("'Create new order' action has been requested");

            if (orderModel == null || orderModel.Products == null || !orderModel.Products.Any())
            {
                return BadRequest("Some product(s) must be specified to make an order");
            }

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var newOrder = await _orderService.CreateOrder(userIdFromToken.Value, orderModel, ct);
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
