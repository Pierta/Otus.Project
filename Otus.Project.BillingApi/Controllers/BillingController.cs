using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Otus.Project.BillingApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;

        public BillingController(ILogger<BillingController> logger)
        {
            _logger = logger;
        }


    }
}
