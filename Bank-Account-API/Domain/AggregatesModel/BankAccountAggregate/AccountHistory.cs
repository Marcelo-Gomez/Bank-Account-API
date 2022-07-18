using Domain.SeedWork;

namespace Domain.AggregatesModel.BankAccountAggregate
{
    public class AccountHistory : BaseAccount
    {
        public double OriginalAmount { get; set; }

        public double AmountAfterDiscounts { get; set; }

        public int Type { get; set; }

        public DateTime TransactionDateUtc { get; set; }
    }
}