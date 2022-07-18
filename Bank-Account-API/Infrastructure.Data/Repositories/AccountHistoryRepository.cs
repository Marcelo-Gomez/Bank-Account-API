using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using Infrastructure.Data.Database;

namespace Infrastructure.Data.Repositories
{
    public class AccountHistoryRepository : IAccountHistoryRepository
    {
        public void InsertAccountTransaction(AccountHistory transactionHistory)
        {
            IEnumerable<AccountHistory> accountHistory = DatabaseConfiguration.GetAllData<AccountHistory>(DatabasePathConst.AccountHistoryPath);

            List<AccountHistory> listAccountHistory = accountHistory.ToList();
            listAccountHistory.Add(transactionHistory);

            DatabaseConfiguration.InsertAllData(listAccountHistory, DatabasePathConst.AccountHistoryPath);
        }
    }
}