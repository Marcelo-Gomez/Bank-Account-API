using Domain.AggregatesModel.BankAccountAggregate;

namespace Application.Commands.Response
{
    public class StatementResponse
    {
        public StatementResponse()
        {
            ExtractDateUtc = DateTime.UtcNow;
        }

        public AccountResponse Account { get; set; }

        public DateTime ExtractDateUtc { get; set; }

        public IEnumerable<AccountHistoryResponse> AccountHistory { get; set; }
    }
}