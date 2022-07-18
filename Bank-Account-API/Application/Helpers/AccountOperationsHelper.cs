using Application.Helpers.Interfaces;
using Application.Queries.Interfaces;
using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using FluentValidation;

namespace Application.Helpers
{
    public class AccountOperationsHelper : IAccountOperationsHelper
    {
        private readonly IAccountQuery _accountQuery;
        private readonly IAccountHistoryRepository _accountHistoryRepository;

        public AccountOperationsHelper(IAccountQuery accountQuery, IAccountHistoryRepository accountHistoryRepository)
        {
            _accountQuery = accountQuery;
            _accountHistoryRepository = accountHistoryRepository;
        }

        public void ValidateAccountExists(Guid accountId)
        {
            if (!_accountQuery.AccountExists(accountId))
            {
                throw new ValidationException(ProblemDetailConst.BadRequestAccountNotExistError);
            }
        }

        public void ValidateAmount(Guid accountId, double amount, double discountRate)
        {
            if (_accountQuery.GetCurrentAccountAmount(accountId) < (amount + discountRate))
            {
                throw new ValidationException(ProblemDetailConst.BadRequestInvalidValueError);
            }
        }

        public double GetNewAccountValue(Guid accountId, double operationAmount, bool isDeposit)
        {
            double currentAccountAmount = _accountQuery.GetCurrentAccountAmount(accountId);

            if (isDeposit)
            {
                return currentAccountAmount + operationAmount;
            }

            return currentAccountAmount - operationAmount;
        }

        public void InsertAccountTransaction(Guid accountId, double depositAmount, double depositedAmount, int transactionType)
        {
            AccountHistory transactionHistory = new()
            {
                AccountId = accountId,
                OriginalAmount = depositAmount,
                AmountAfterDiscounts = depositedAmount,
                Type = transactionType,
                TransactionDateUtc = DateTime.UtcNow
            };

            _accountHistoryRepository.InsertAccountTransaction(transactionHistory);
        }
    }
}