using Application.Validators;

namespace UnitTest.Application.Validators
{
    public class TransferAccounRequestValidatorTest
    {
        private TransferAccountRequestValidator _transferAccounRequestValidator;
        private TransferAccountRequest _transferAccounRequest;
        private readonly Guid _accountId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _transferAccounRequestValidator = new TransferAccountRequestValidator();
            _transferAccounRequest = GetTransferAccountRequest();
        }

        [Test]
        [TestCase(10, ExpectedResult = true, Description = "Valid value")]
        [TestCase(1, ExpectedResult = false, Description = "Invalid value")]
        [TestCase(0, ExpectedResult = false, Description = "Invalid value")]
        [TestCase(-1, ExpectedResult = false, Description = "Invalid value")]
        public bool Validate_WithPossibleAmounts_IsValid(double transferAmount)
        {
            //Arrange
            _transferAccounRequest.TransferAmount = transferAmount;

            //Assert
            return _transferAccounRequestValidator.Validate(_transferAccounRequest).IsValid;
        }

        [Test]
        [TestCase(ExpectedResult = false, Description = "Invalid AccountId")]
        public bool Validate_WithEqualsAccountIds_IsInvalid()
        {
            //Arrange
            _transferAccounRequest.AccountId = _accountId;
            _transferAccounRequest.AccountReceiveId = _accountId;
            _transferAccounRequest.TransferAmount = 100;

            //Assert
            return _transferAccounRequestValidator.Validate(_transferAccounRequest).IsValid;
        }

        [Test]
        [TestCase(ExpectedResult = true, Description = "Valid AccountId")]
        public bool Validate_WithDifferentAccountIds_IsValid()
        {
            //Arrange
            _transferAccounRequest.AccountId = _accountId;
            _transferAccounRequest.AccountReceiveId = Guid.NewGuid();
            _transferAccounRequest.TransferAmount = 100;

            //Assert
            return _transferAccounRequestValidator.Validate(_transferAccounRequest).IsValid;
        }

        private static TransferAccountRequest GetTransferAccountRequest()
        {
            return new TransferAccountRequest
            {
                AccountId = Guid.NewGuid(),
                AccountReceiveId = Guid.NewGuid()
            };
        }
    }
}