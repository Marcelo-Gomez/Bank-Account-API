using Api.Controllers;
using Application.Commands.Response;
using Application.Queries.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace UnitTest.Api.Controllers
{
    public class TransactionControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IStatementQuery> _statementQueryMock;
        private TransactionController _transactionController;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _statementQueryMock = new Mock<IStatementQuery>();

            _transactionController = new TransactionController(
                _mediatorMock.Object,
                _statementQueryMock.Object
            );
        }

        [Test]
        public void Deposit_WithValidDeposit_ResponseIsSuccess()
        {
            //Arrange
            DepositRequest depositRequest = new Fixture().Create<DepositRequest>();

            //Setup
            _mediatorMock.Setup(_ => _.Send(It.IsAny<DepositRequest>(), It.IsAny<CancellationToken>()));

            //Act
            var response = _transactionController.Deposit(depositRequest).Result;
            var okResult = (OkResult)response;

            //Assert
            response.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public void Withdraw_WithValidWithdraw_ResponseIsSuccess()
        {
            //Arrange
            WithdrawRequest withdrawRequest = new Fixture().Create<WithdrawRequest>();

            //Setup
            _mediatorMock.Setup(_ => _.Send(It.IsAny<WithdrawRequest>(), It.IsAny<CancellationToken>()));

            //Act
            var response = _transactionController.Withdraw(withdrawRequest).Result;
            var okResult = (OkResult)response;

            //Assert
            response.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public void TransferToAccount_WithValidTransfer_ResponseIsSuccess()
        {
            //Arrange
            TransferAccountRequest transferAccountRequest = new Fixture().Create<TransferAccountRequest>();

            //Setup
            _mediatorMock.Setup(_ => _.Send(It.IsAny<TransferAccountRequest>(), It.IsAny<CancellationToken>()));

            //Act
            var response = _transactionController.TransferToAccount(transferAccountRequest).Result;
            var okResult = (OkResult)response;

            //Assert
            response.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public void Statement_WithValidAccountId_ResponseValidStatement()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            StatementResponse statementResponse = new Fixture().Create<StatementResponse>();

            //Setup
            _statementQueryMock.Setup(_ => _.GetStatement(It.IsAny<Guid>())).Returns(statementResponse);

            //Act
            var response = _transactionController.Statement(accountId).Result;
            var okResult = (OkObjectResult)response;
            var result = (StatementResponse)okResult.Value;

            //Assert
            response.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Account.Should().NotBeNull();
            result.AccountHistory.Should().NotBeNull();
            result.ExtractDateUtc.Should().Be(statementResponse.ExtractDateUtc);
        }

        [Test]
        public void Statement_WithInvalidAccountId_ResponseBadRequest()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            StatementResponse statementResponse = null;

            //Setup
            _statementQueryMock.Setup(_ => _.GetStatement(It.IsAny<Guid>())).Returns(statementResponse);

            //Act
            var response = _transactionController.Statement(accountId).Result;
            var okResult = (BadRequestObjectResult)response;

            //Assert
            response.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}