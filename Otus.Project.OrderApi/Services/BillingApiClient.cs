using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Otus.Project.OrderApi.Settings;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public class BillingApiClient : IBillingApiClient
    {
        private readonly ExternalServices _externalServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string AuthorizationHeader = "Authorization";
        private const string BearerPrefix = "Bearer ";

        public BillingApiClient(IOptions<ExternalServices> externalServices,
            IHttpContextAccessor httpContextAccessor)
        {
            _externalServices = externalServices.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> WithdrawMoney(Guid userId, decimal value, CancellationToken ct)
        {
            // Re-use an existing JWT from the original request to the API
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers
                .TryGetValue(AuthorizationHeader, out StringValues requestHeader)
                    ? requestHeader[0]
                    : null;
            
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new InvalidOperationException("Token is not found in an original request");
            }

            var token = authorizationHeader.Replace(BearerPrefix, "");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            string relativeUri = $"/Billing/withdraw/{value}";
            var requestUri = new Uri(new Uri(_externalServices.BillingApi.Url), relativeUri);
            var result = await client.PutAsync(requestUri, null, ct);

            return result.StatusCode != HttpStatusCode.BadRequest; // Money is enough
        }
    }
}
