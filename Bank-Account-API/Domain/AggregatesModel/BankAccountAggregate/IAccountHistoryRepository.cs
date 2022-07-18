namespace Domain.AggregatesModel.BankAccountAggregate
{
    public interface IAccountHistoryRepository
    {
        void InsertAccountTransaction(AccountHistory transactionHistory);
    }
}