namespace UnitTest.Application.Validators
{
    public class WithdrawRequestValidatorTest
    {
        private WithdrawRequestValidator _withdrawRequestValidator;
        private WithdrawRequest _withdrawRequest;

        [SetUp]
        public void Setup()
        {
            _withdrawRequestValidator = new WithdrawRequestValidator();
            _withdrawRequest = GetWithdrawRequest();
        }

        [Test]
        [TestCase(10, ExpectedResult = true, Description = "Valid value")]
        [TestCase(4, ExpectedResult = false, Description = "Invalid value")]
        [TestCase(0, ExpectedResult = false, Description = "Invalid value")]
        [TestCase(-1, ExpectedResult = false, Description = "Invalid value")]
        public bool Validate_WithPossibleAmounts_IsValid(double withdrawAmount)
        {
            //Arrange
            _withdrawRequest.WithdrawAmount = withdrawAmount;

            //Assert
            return _withdrawRequestValidator.Validate(_withdrawRequest).IsValid;
        }

        private static WithdrawRequest GetWithdrawRequest()
        {
            return new WithdrawRequest
            {
                AccountId = Guid.NewGuid()
            };
        }
    }
}