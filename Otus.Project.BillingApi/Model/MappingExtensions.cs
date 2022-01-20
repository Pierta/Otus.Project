using Otus.Project.Domain.Model;

namespace Otus.Project.BillingApi.Model
{
    public static class MappingExtensions
    {
        public static BillingAccountVm ConvertToVm(this BillingAccount billingAccount)
        {
            return new BillingAccountVm
            {
                Id = billingAccount.Id,
                CreatedDate = billingAccount.CreatedDate,
                UpdatedDate = billingAccount.UpdatedDate,
                UserId = billingAccount.UserId,
                Balance = billingAccount.Balance
            };
        }
    }
}
