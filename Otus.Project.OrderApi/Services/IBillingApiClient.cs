using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public interface IBillingApiClient
    {
        Task<bool> WithdrawMoney(Guid userId, decimal value, CancellationToken ct);
    }
}
