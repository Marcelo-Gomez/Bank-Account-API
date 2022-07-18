namespace Application.Commands.Response
{
    public class AccountHistoryResponse
    {
        public double OriginalAmount { get; set; }

        public double AmountAfterDiscounts { get; set; }

        public int Type { get; set; }

        public DateTime TransactionDateUtc { get; set; }
    }
}