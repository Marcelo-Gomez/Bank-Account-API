using Application.Helpers;
using Application.Queries.Interfaces;
using Domain.Enums;

namespace UnitTest.Application.Helpers
{
    public class AccountOperationsHelperTest
    {
        private Mock<IAccountQuery> _accountQuery;
        private Mock<IAccountHistoryRepository> _accountHistoryRepository;
        private AccountOperationsHelper _accountOperationsHelper;

        [SetUp]
        public void Setup()
        {
            _accountQuery = new Mock<IAccountQuery>();
            _accountHistoryRepository = new Mock<IAccountHistoryRepository>();

            _accountOperationsHelper = new AccountOperationsHelper(
                _accountQuery.Object,
                _accountHistoryRepository.Object
            );
        }

        [Test]
        public void ValidateAccountExists_WithValidAccountId_ResponseIsSuccess()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            bool isValidAccount = true;

            //Setup
            _accountQuery.Setup(_ => _.AccountExists(It.IsAny<Guid>())).Returns(isValidAccount);

            //Act
            _accountOperationsHelper.ValidateAccountExists(accountId);

            //Asserts
            Assert.Pass();
        }

        [Test]
        public void ValidateAccountExists_WithInvalidAccountId_ResponseBadRequest()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            bool isValidAccount = false;

            //Setup
            _accountQuery.Setup(_ => _.AccountExists(It.IsAny<Guid>())).Returns(isValidAccount);

            //Act - Asserts
            var exception = Assert.Throws<ValidationException>(() => _accountOperationsHelper.ValidateAccountExists(accountId));
        }

        [Test]
        public void ValidateAmount_WithValidAccountId_ResponseIsSuccess()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            double currentAccountAmount = 100.0;
            double amount = 10.0;
            double discountRate = 1;

            //Setup
            _accountQuery.Setup(_ => _.GetCurrentAccountAmount(It.IsAny<Guid>())).Returns(currentAccountAmount);

            //Act
            _accountOperationsHelper.ValidateAmount(accountId, amount, discountRate);

            //Asserts
            Assert.Pass();
        }

        [Test]
        public void ValidateAmount_WithInvalidAccountId_ResponseBadRequest()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            double currentAccountAmount = 100.0;
            double amount = 100.0;
            double discountRate = 1;

            //Setup
            _accountQuery.Setup(_ => _.GetCurrentAccountAmount(It.IsAny<Guid>())).Returns(currentAccountAmount);

            //Act - Asserts
            var exception = Assert.Throws<ValidationException>(() => _accountOperationsHelper.ValidateAmount(accountId, amount, discountRate));
        }

        [Test]
        public void GetNewAccountValue_WithDeposit_ResponseGreaterAmountAccount()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            double currentAccountAmount = 100.0;
            double operationAmount = 10.0;
            bool isDeposit = true;

            //Setup
            _accountQuery.Setup(_ => _.GetCurrentAccountAmount(It.IsAny<Guid>())).Returns(currentAccountAmount);

            //Act
            var response = _accountOperationsHelper.GetNewAccountValue(accountId, operationAmount, isDeposit);

            //Asserts
            response.Should().Be(currentAccountAmount + operationAmount);
        }

        [Test]
        public void GetNewAccountValue_WithDeposit_ResponseLesserAmountAccount()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            double currentAccountAmount = 100.0;
            double operationAmount = 10.0;
            bool isDeposit = false;

            //Setup
            _accountQuery.Setup(_ => _.GetCurrentAccountAmount(It.IsAny<Guid>())).Returns(currentAccountAmount);

            //Act
            var response = _accountOperationsHelper.GetNewAccountValue(accountId, operationAmount, isDeposit);

            //Asserts
            response.Should().Be(currentAccountAmount - operationAmount);
        }

        [Test]
        public void InsertAccountTransaction_WithValidAccountHistory_ResponseIsSuccess()
        {
            //Arrange
            Guid accountId = Guid.NewGuid();
            double depositAmount = 100.0;
            double depositedAmount = 90.0;

            //Setup
            _accountHistoryRepository.Setup(_ => _.InsertAccountTransaction(It.IsAny<AccountHistory>()));

            //Act
            _accountOperationsHelper.InsertAccountTransaction(accountId, depositAmount, depositedAmount, (int)TransactionTypeEnum.Deposit);

            //Asserts
            Assert.Pass();
        }
    }
}