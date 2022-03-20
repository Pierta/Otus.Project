using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.DeliveryApi.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.DeliveryApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DeliverySlotsController : ControllerBase
    {
        private readonly ILogger<DeliverySlotsController> _logger;
        private readonly IDeliveryService _deliveryService;

        public DeliverySlotsController(ILogger<DeliverySlotsController> logger,
            IDeliveryService deliveryService)
        {
            _logger = logger;
            _deliveryService = deliveryService;
        }

        [HttpGet("{orderId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid orderId, CancellationToken ct)
        {
            _logger.LogInformation("'Get delivery slot by order id' action has been requested");

            try
            {
                var existingSlot = await _deliveryService.GetDeliverySlotByOrderId(orderId, ct);
                return Ok(existingSlot);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliverySlots(CancellationToken ct)
        {
            _logger.LogInformation("'Get a list of delivery slots' action has been requested");

            var slots = await _deliveryService.GetDeliverySlots(ct);
            return Ok(slots);
        }
    }
}
