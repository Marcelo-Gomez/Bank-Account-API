namespace Domain.AggregatesModel.BankAccountAggregate
{
    public interface IAccountRepository
    {
        void UpdateAccountAmount(Guid accountId, double accountAmount);
    }
}