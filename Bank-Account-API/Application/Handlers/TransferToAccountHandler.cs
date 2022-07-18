using Application.Commands.Request;
using Application.Helpers.Interfaces;
using Domain.AggregatesModel.BankAccountAggregate;
using Domain.Consts;
using Domain.Enums;
using MediatR;

namespace Application.Handlers
{
    public class TransferToAccountHandler : IRequestHandler<TransferAccountRequest>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountOperationsHelper _accountValidationHelper;

        public TransferToAccountHandler(IAccountRepository accountRepository, IAccountOperationsHelper accountValidationHelper)
        {
            _accountRepository = accountRepository;
            _accountValidationHelper = accountValidationHelper;
        }

        public Task<Unit> Handle(TransferAccountRequest request, CancellationToken cancellationToken)
        {
            _accountValidationHelper.ValidateAccountExists(request.AccountId);
            _accountValidationHelper.ValidateAccountExists(request.AccountReceiveId);

            _accountValidationHelper.ValidateAmount(request.AccountId, request.TransferAmount, DiscountRateConst.TransferRate);

            double transferAmount = TransferDiscountCalculation(request.TransferAmount);

            double newCurrentAccountAmount = _accountValidationHelper.GetNewAccountValue(request.AccountId, transferAmount, false);
            _accountRepository.UpdateAccountAmount(request.AccountId, newCurrentAccountAmount);

            double newReceiveAccountAmount = _accountValidationHelper.GetNewAccountValue(request.AccountReceiveId, request.TransferAmount, true);
            _accountRepository.UpdateAccountAmount(request.AccountReceiveId, newReceiveAccountAmount);

            _accountValidationHelper.InsertAccountTransaction(request.AccountId, request.TransferAmount,
                transferAmount, (int)TransactionTypeEnum.MadeTransfer);

            _accountValidationHelper.InsertAccountTransaction(request.AccountReceiveId, request.TransferAmount,
                request.TransferAmount, (int)TransactionTypeEnum.ReceivedTransfer);

            return Task.FromResult(Unit.Value);
        }

        private static double TransferDiscountCalculation(double transferAmount)
            => transferAmount + DiscountRateConst.TransferRate;
    }
}