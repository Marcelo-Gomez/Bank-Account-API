using Application.Commands.Request;
using Application.Helpers.Interfaces;
using Application.Queries.Interfaces;
using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Enums;
using MediatR;

namespace Application.Handlers
{
    public class DepositHandler : IRequestHandler<DepositRequest>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountOperationsHelper _accountValidationHelper;

        public DepositHandler(IAccountRepository accountRepository, IAccountOperationsHelper accountValidationHelper)
        {
            _accountRepository = accountRepository;
            _accountValidationHelper = accountValidationHelper;
        }

        public Task<Unit> Handle(DepositRequest request, CancellationToken cancellationToken)
        {
            _accountValidationHelper.ValidateAccountExists(request.AccountId);

            double depositedAmount = DepositDiscountCalculation(request.DepositAmount);

            double newCurrentAccountAmount = _accountValidationHelper.GetNewAccountValue(request.AccountId, depositedAmount, true);

            _accountRepository.UpdateAccountAmount(request.AccountId, newCurrentAccountAmount);

            _accountValidationHelper.InsertAccountTransaction(request.AccountId, request.DepositAmount, 
                depositedAmount, (int)TransactionTypeEnum.Deposit);

            return Task.FromResult(Unit.Value);
        }

        private static double DepositDiscountCalculation(double depositAmount)
        {
            double discountAmountDeposited = depositAmount / 100;

            return depositAmount - discountAmountDeposited;
        }
    }
}