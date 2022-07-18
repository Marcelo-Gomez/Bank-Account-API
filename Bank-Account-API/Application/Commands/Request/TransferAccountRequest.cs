using MediatR;

namespace Application.Commands.Request
{
    public class TransferAccountRequest : IRequest
    {
        public Guid AccountId { get; set; }

        public Guid AccountReceiveId { get; set; }

        public double TransferAmount { get; set; }
    }
}