namespace UnitTest.Application.Handlers
{
    public class DepositHandlerTest
    {
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IAccountOperationsHelper> _accountOperationsHelper;
        private readonly CancellationToken _cancellationToken;
        private DepositHandler _depositHandler;

        [SetUp]
        public void Setup()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _accountOperationsHelper = new Mock<IAccountOperationsHelper>();

            _depositHandler = new DepositHandler(
                _accountRepository.Object,
                _accountOperationsHelper.Object
            );
        }

        [Test]
        public void Handle_WithValidDeposit_ResponseIsSuccess()
        {
            //Arrange
            double newCurrentAccountAmount = 190.0;
            DepositRequest depositRequest = new Fixture().Build<DepositRequest>()
                                                         .With(p => p.DepositAmount, 100.0)
                                                         .Create();

            //Setup
            _accountOperationsHelper.Setup(_ => _.ValidateAccountExists(It.IsAny<Guid>()));
            _accountOperationsHelper.Setup(_ => _.GetNewAccountValue(It.IsAny<Guid>(), It.IsAny<double>(), It.IsAny<bool>())).Returns(newCurrentAccountAmount);
            _accountRepository.Setup(_ => _.UpdateAccountAmount(It.IsAny<Guid>(), It.IsAny<double>()));
            _accountOperationsHelper.Setup(_ => _.InsertAccountTransaction(It.IsAny<Guid>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()));

            //Act
            _depositHandler.Handle(depositRequest, _cancellationToken);

            //Asserts
            Assert.Pass();
        }
    }
}