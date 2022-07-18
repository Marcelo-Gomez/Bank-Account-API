using Application.Commands.Request;
using FluentValidation;

namespace Application.Validators
{
    public class WithdrawRequestValidator : AbstractValidator<WithdrawRequest>
    {
        public WithdrawRequestValidator()
        {
            RuleFor(r => r.WithdrawAmount)
                .GreaterThan(4);
        }
    }
}