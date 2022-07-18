namespace IntegrationTest.Endpoints.Transactions
{
    public class DepositEndpointTest
    {
        private readonly string endpointPath = "/Api/Bank/Transaction/Deposit";

        [Test]
        public async Task Deposit_WithValidDepositRequest_ResponseIsOK()
        {
            // Arrange
            DepositRequest depositRequest = new Fixture().Build<DepositRequest>()
                                                         .With(p => p.AccountId, new Guid("f1b0e5b7-dcc3-4db8-b8d1-a080f003ac43"))
                                                         .With(p => p.DepositAmount, 10)
                                                         .Create();
            string json = JsonConvert.SerializeObject(depositRequest);
            StringContent bodyRequest = new (json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Deposit_WithInvalidAccountId_ResponseIsBadRequest()
        {
            // Arrange
            DepositRequest depositRequest = new Fixture().Build<DepositRequest>()
                                                         .With(p => p.DepositAmount, 10)
                                                         .Create();
            string json = JsonConvert.SerializeObject(depositRequest);
            StringContent bodyRequest = new(json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Deposit_WithInvalidDepositAmount_ResponseIsBadRequest()
        {
            // Arrange
            DepositRequest depositRequest = new Fixture().Build<DepositRequest>()
                                                         .With(p => p.AccountId, new Guid("f1b0e5b7-dcc3-4db8-b8d1-a080f003ac43"))
                                                         .With(p => p.DepositAmount, 0.5)
                                                         .Create();
            string json = JsonConvert.SerializeObject(depositRequest);
            StringContent bodyRequest = new(json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}