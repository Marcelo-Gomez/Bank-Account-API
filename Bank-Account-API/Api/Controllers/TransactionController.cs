using Application.Commands.Request;
using Application.Commands.Response;
using Application.Queries.Interfaces;
using Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/Bank/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStatementQuery _statementQuery;

        public TransactionController(IMediator mediator, IStatementQuery statementQuery)
        {
            _mediator = mediator;
            _statementQuery = statementQuery;
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit([FromBody] DepositRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost("TransferToAccount")]
        public async Task<ActionResult> TransferToAccount([FromBody] TransferAccountRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("Statement/{accountId}")]
        public async Task<ActionResult> Statement(Guid accountId)
        {
            StatementResponse response = _statementQuery.GetStatement(accountId);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(ProblemDetailConst.BadRequestAccountNotExistError);
        }
    }
}