using Otus.Project.BillingApi.Model;
using Otus.Project.MessageBus.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.BillingApi.Services
{
    public interface IBillingAccountService
    {
        Task<BillingAccountVm> CreateNewBillingAccountIfNotExist(Guid userId, CancellationToken ct);

        Task<decimal> GetCurrentBalance(Guid userId, CancellationToken ct);

        Task<decimal> TopUpBalance(Guid userId, decimal value, CancellationToken ct);

        Task<decimal> WithdrawMoney(Guid userId, decimal value, CancellationToken ct);

        Task PayForTheOrder(DeliveryReserved deliveryModel, CancellationToken ct);
    }
}
