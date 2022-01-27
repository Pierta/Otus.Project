using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public interface IBillingApiClient
    {
        Task CreateNewBillingAccount(Guid userId, CancellationToken ct);
    }
}
