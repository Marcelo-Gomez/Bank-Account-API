namespace Domain.AggregatesModel.BankAccountAggregate
{
    public class BankAccount
    {
        public Account Account { get; set; }

        public IEnumerable<AccountHistory> AccountHistory { get; set; }
    }
}