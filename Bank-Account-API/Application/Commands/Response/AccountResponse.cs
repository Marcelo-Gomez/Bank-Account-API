using Domain.SeedWork;

namespace Domain.AggregatesModel.BankAccountAggregate
{
    public class AccountResponse : BaseAccount
    {
        public double TotalAmount { get; set; }
    }
}