using MediatR;

namespace Application.Commands.Request
{
    public class DepositRequest : IRequest
    {
        public Guid AccountId { get; set; }

        public double DepositAmount { get; set; }
    }
}