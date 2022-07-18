using Application.Commands.Request;
using FluentValidation;

namespace Application.Validators
{
    public class DepositRequestValidator : AbstractValidator<DepositRequest>
    {
        public DepositRequestValidator()
        {
            RuleFor(r => r.DepositAmount)
                    .GreaterThanOrEqualTo(1);
        }
    }
}