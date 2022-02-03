using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.OrderApi.Model;
using Otus.Project.OrderApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger,
            IHttpContextAccessor httpContextAccessor,
            IOrderService orderService)
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

        [HttpPost("Idempotent")]
        public async Task<IActionResult> PostIdempotent([FromBody] OrderModel orderModel, CancellationToken ct)
        {
            _logger.LogInformation("'Create new order (idempotent)' action has been requested");

            if (orderModel == null || orderModel.Products == null || !orderModel.Products.Any())
            {
                return BadRequest("Some product(s) must be specified to make an order");
            }

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var newOrder = await _orderService.CreateOrderIdempotent(userIdFromToken.Value, orderModel, ct);
                return Ok(newOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{orderId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid orderId, CancellationToken ct)
        {
            _logger.LogInformation("'Get order by id' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            try
            {
                var existingOrder = await _orderService.GetOrderById(orderId, ct);
                if (existingOrder.UserId != userIdFromToken.Value)
                {
                    return StatusCode(403, "You have no access to this order");
                }

                return Ok(existingOrder);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogInformation("'Get a list of user orders' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            var userOrders = await _orderService.GetUserOrders(userIdFromToken.Value, ct);
            return Ok(userOrders);
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts(CancellationToken ct)
        {
            _logger.LogInformation("'Get a list of products' action has been requested");

            var products = await _orderService.GetProducts(ct);
            return Ok(products);
        }
    }
}
