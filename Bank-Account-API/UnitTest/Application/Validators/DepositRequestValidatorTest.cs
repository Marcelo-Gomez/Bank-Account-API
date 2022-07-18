namespace UnitTest.Application.Validators
{
    public class DepositRequestValidatorTest
    {
        private DepositRequestValidator _depositRequestValidator;
        private DepositRequest _depositRequest;

        [SetUp]
        public void Setup()
        {
            _depositRequestValidator = new DepositRequestValidator();
            _depositRequest = GetDepositRequest();
        }

        [Test]
        [TestCase(10, ExpectedResult = true, Description = "Valid value")]
        [TestCase(1, ExpectedResult = true, Description = "Valid value")]
        [TestCase(0.1, ExpectedResult = false, Description = "Invalid value")]
        [TestCase(0, ExpectedResult = false, Description = "Invalid value")]
        [TestCase(-1, ExpectedResult = false, Description = "Invalid value")]
        public bool Validate_WithPossibleAmounts_IsValid(double depositAmount)
        {
            //Arrange
            _depositRequest.DepositAmount = depositAmount;

            //Assert
            return _depositRequestValidator.Validate(_depositRequest).IsValid;
        }

        private static DepositRequest GetDepositRequest()
        {
            return new DepositRequest
            {
                AccountId = Guid.NewGuid()
            };
        }
    }
}