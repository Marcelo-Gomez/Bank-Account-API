using Application.Commands.Response;
using Application.Queries.Interfaces;
using AutoMapper;
using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using Infrastructure.Data.Database;

namespace Application.Queries
{
    public class StatementQuery : IStatementQuery
    {
        private readonly IMapper _mapper;

        public StatementQuery(IMapper mapper)
        {
            _mapper = mapper;
        }

        public StatementResponse? GetStatement(Guid accountId)
        {
            Account account = DatabaseConfiguration.GetAllData<Account>(DatabasePathConst.AccountPath)
                .Where(x => x.AccountId == accountId).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            IEnumerable<AccountHistory> accountHistory = DatabaseConfiguration.GetAllData<AccountHistory>(DatabasePathConst.AccountHistoryPath)
                .Where(x => x.AccountId == accountId);

            BankAccount BankAccount = new()
            {
                Account = account,
                AccountHistory = accountHistory
            };

            return _mapper.Map<StatementResponse>(BankAccount);
        }
    }
}