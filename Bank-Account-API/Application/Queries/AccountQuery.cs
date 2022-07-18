using Application.Queries.Interfaces;
using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using Infrastructure.Data.Database;

namespace Application.Queries
{
    public class AccountQuery : IAccountQuery
    {
        public bool AccountExists(Guid AccountId)
        {
            IEnumerable<AccountResponse> accounts = DatabaseConfiguration.GetAllData<AccountResponse>(DatabasePathConst.AccountPath);

            return accounts.Any(x => x.AccountId == AccountId);
        }

        public double GetCurrentAccountAmount(Guid accountId)
        {
            IEnumerable<AccountResponse> accounts = DatabaseConfiguration.GetAllData<AccountResponse>(DatabasePathConst.AccountPath);

            return accounts.Where(x => x.AccountId == accountId)
                           .Select(t => t.TotalAmount)
                           .FirstOrDefault();
        }
    }
}