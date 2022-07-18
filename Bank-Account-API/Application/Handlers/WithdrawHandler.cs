using Application.Commands.Request;
using Application.Helpers.Interfaces;
using Application.Queries.Interfaces;
using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawRequest, Unit>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountOperationsHelper _accountValidationHelper;

        public WithdrawHandler(IAccountRepository accountRepository, IAccountOperationsHelper accountValidationHelper)
        {
            _accountRepository = accountRepository;
            _accountValidationHelper = accountValidationHelper;
        }

        public Task<Unit> Handle(WithdrawRequest request, CancellationToken cancellationToken)
        {
            _accountValidationHelper.ValidateAccountExists(request.AccountId);

            _accountValidationHelper.ValidateAmount(request.AccountId, request.WithdrawAmount, DiscountRateConst.WithdrawRate);

            double amountWithdrawn = WithdrawDiscountCalculation(request.WithdrawAmount);

            double newCurrentAccountAmount = _accountValidationHelper.GetNewAccountValue(request.AccountId, amountWithdrawn, false);

            _accountRepository.UpdateAccountAmount(request.AccountId, newCurrentAccountAmount);

            _accountValidationHelper.InsertAccountTransaction(request.AccountId, request.WithdrawAmount,
                amountWithdrawn, (int)TransactionTypeEnum.Withdraw);

            return Task.FromResult(Unit.Value);
        }

        private static double WithdrawDiscountCalculation(double withdrawAmount)
            => withdrawAmount + DiscountRateConst.WithdrawRate;
    }
}