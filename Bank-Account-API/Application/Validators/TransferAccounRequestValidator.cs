using Application.Commands.Request;
using FluentValidation;

namespace Application.Validators
{
    public class TransferAccountRequestValidator : AbstractValidator<TransferAccountRequest>
    {
        public TransferAccountRequestValidator()
        {
            RuleFor(r => r.AccountId)
                .NotEqual(x => x.AccountReceiveId);

            RuleFor(r => r.AccountReceiveId)
                .NotEqual(x => x.AccountId);

            RuleFor(r => r.TransferAmount)
                .GreaterThan(1);
        }
    }
}