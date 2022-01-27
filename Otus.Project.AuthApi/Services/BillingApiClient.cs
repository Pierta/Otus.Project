using Microsoft.Extensions.Options;
using Otus.Project.AuthApi.Settings;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public class BillingApiClient : IBillingApiClient
    {
        private readonly ExternalServices _externalServices;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public BillingApiClient(IOptions<ExternalServices> externalServices,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _externalServices = externalServices.Value;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task CreateNewBillingAccount(Guid userId, CancellationToken ct)
        {
            using var client = new HttpClient();
            var token = _jwtTokenGenerator.GenerateJwtToken(userId);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            const string relativeUri = "/Billing";
            var requestUri = new Uri(new Uri(_externalServices.BillingApi.Url), relativeUri); 
            var result = await client.PostAsync(requestUri, null, ct);
        }
    }
}
