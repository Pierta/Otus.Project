using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.StockApi.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.StockApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger<StocksController> _logger;
        private readonly IStockService _stockService;

        public StocksController(ILogger<StocksController> logger,
            IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        [HttpGet("{productId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid productId, CancellationToken ct)
        {
            _logger.LogInformation("'Get stock by product id' action has been requested");

            try
            {
                var existingStock = await _stockService.GetStockByProductId(productId, ct);
                return Ok(existingStock);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks(CancellationToken ct)
        {
            _logger.LogInformation("'Get a list of stocks' action has been requested");

            var stocks = await _stockService.GetStocks(ct);
            return Ok(stocks);
        }
    }
}
