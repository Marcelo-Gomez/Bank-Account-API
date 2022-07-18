using Domain.SeedWork;

namespace Domain.AggregatesModel.BankAccountAggregate
{
    public class Account : BaseAccount
    {
        public double TotalAmount { get; set; }
    }
}