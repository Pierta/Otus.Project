using Medallion.Threading;
using Otus.Project.BillingApi.Model;
using Otus.Project.Domain.Model;
using Otus.Project.Orm.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.BillingApi.Services
{
    public class BillingAccountService : IBillingAccountService
    {
        private readonly IRepository<BillingAccount, Guid> _billingAccountRepository;
        private readonly IDistributedLockProvider _distributedLockProvider;

        public BillingAccountService(IRepository<BillingAccount, Guid> billingAccountRepository,
            IDistributedLockProvider distributedLockProvider)
        {
            _billingAccountRepository = billingAccountRepository;
            _distributedLockProvider = distributedLockProvider;
        }

        public async Task<BillingAccountVm> CreateNewBillingAccountIfNotExist(Guid userId, CancellationToken ct)
        {
            Expression<Func<BillingAccount, bool>> selectByUserIdSpec = billingAccount => billingAccount.UserId == userId;            
            var existingBillingAccount = await _billingAccountRepository.FindByExpression(selectByUserIdSpec, ct);
            if (existingBillingAccount == null)
            {
                var now = DateTime.UtcNow;
                existingBillingAccount = new BillingAccount
                {
                    CreatedDate = now,
                    UpdatedDate = now,
                    UserId = userId,
                    Balance = 0m
                };
                _billingAccountRepository.Add(existingBillingAccount);
                await _billingAccountRepository.CommitChangesAsync(ct);
            }

            return existingBillingAccount.ConvertToVm();
        }

        public async Task<decimal> GetCurrentBalance(Guid userId, CancellationToken ct)
        {
            Expression<Func<BillingAccount, bool>> selectByUserIdSpec = billingAccount => billingAccount.UserId == userId;
            var existingBillingAccount = await _billingAccountRepository.FindByExpression(selectByUserIdSpec, ct);
            if (existingBillingAccount == null)
            {
                throw new KeyNotFoundException($"Billing account for user with Id = '{userId}' is not found in a database!");
            }

            return existingBillingAccount.Balance;
        }

        public async Task<decimal> TopUpBalance(Guid userId, decimal value, CancellationToken ct)
        {
            Expression<Func<BillingAccount, bool>> selectByUserIdSpec = billingAccount => billingAccount.UserId == userId;

            using (_distributedLockProvider.AcquireLock($"BillingAccountForUserId_{userId}", TimeSpan.FromMinutes(1), ct))
            {
                var existingBillingAccount = await _billingAccountRepository.FindByExpression(selectByUserIdSpec, ct);
                if (existingBillingAccount == null)
                {
                    throw new KeyNotFoundException($"Billing account for user with Id = '{userId}' is not found in a database!");
                }

                existingBillingAccount.Balance += value;
                _billingAccountRepository.Update(existingBillingAccount);
                await _billingAccountRepository.CommitChangesAsync(ct);

                return existingBillingAccount.Balance;
            }
        }

        public async Task<decimal> WithdrawMoney(Guid userId, decimal value, CancellationToken ct)
        {
            Expression<Func<BillingAccount, bool>> selectByUserIdSpec = billingAccount => billingAccount.UserId == userId;

            using (_distributedLockProvider.AcquireLock($"BillingAccountForUserId_{userId}", TimeSpan.FromMinutes(1), ct))
            {
                var existingBillingAccount = await _billingAccountRepository.FindByExpression(selectByUserIdSpec, ct);
                if (existingBillingAccount == null)
                {
                    throw new KeyNotFoundException($"Billing account for user with Id = '{userId}' is not found in a database!");
                }

                if (existingBillingAccount.Balance - value < 0)
                {
                    throw new InvalidOperationException($"It's not enough money on user's balance: {existingBillingAccount.Balance}");
                }

                existingBillingAccount.Balance -= value;
                _billingAccountRepository.Update(existingBillingAccount);
                await _billingAccountRepository.CommitChangesAsync(ct);

                return existingBillingAccount.Balance;
            }
        }
    }
}
