using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using Infrastructure.Data.Database;

namespace Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public void UpdateAccountAmount(Guid accountId, double accountAmount)
        {
            IEnumerable<Account> accounts = DatabaseConfiguration.GetAllData<Account>(DatabasePathConst.AccountPath);

            Account account = accounts.FirstOrDefault(x => x.AccountId == accountId);
            account.TotalAmount = accountAmount;

            accounts.Select(t => { t = account; return t; })
                    .Where(x => x.AccountId == accountId)
                    .ToList();

            DatabaseConfiguration.InsertAllData(accounts, DatabasePathConst.AccountPath);
        }
    }
}