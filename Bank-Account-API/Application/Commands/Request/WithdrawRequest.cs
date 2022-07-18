using MediatR;

namespace Application.Commands.Request
{
    public class WithdrawRequest : IRequest
    {
        public Guid AccountId { get; set; }

        public double WithdrawAmount { get; set; }
    }
}