namespace IntegrationTest.Endpoints.Transactions
{
    public class WithdrawEndpointTest
    {
        private readonly string endpointPath = "/Api/Bank/Transaction/Withdraw";

        [Test]
        public async Task Withdraw_WithValidWithdrawRequest_ResponseIsOK()
        {
            // Arrange
            WithdrawRequest withdrawRequest = new Fixture().Build<WithdrawRequest>()
                                                           .With(p => p.AccountId, new Guid("c89290c3-975d-40c5-a097-d21665cdaf25"))
                                                           .With(p => p.WithdrawAmount, 4.5)
                                                           .Create();
            string json = JsonConvert.SerializeObject(withdrawRequest);
            StringContent bodyRequest = new (json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Withdraw_WithInvalidAccountId_ResponseIsBadRequest()
        {
            // Arrange
            WithdrawRequest withdrawRequest = new Fixture().Build<WithdrawRequest>()
                                                           .With(p => p.WithdrawAmount, 4.5)
                                                           .Create();
            string json = JsonConvert.SerializeObject(withdrawRequest);
            StringContent bodyRequest = new(json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Withdraw_WithInvalidWithdrawAmount_ResponseIsBadRequest()
        {
            // Arrange
            WithdrawRequest withdrawRequest = new Fixture().Build<WithdrawRequest>()
                                                           .With(p => p.AccountId, new Guid("c89290c3-975d-40c5-a097-d21665cdaf25"))
                                                           .With(p => p.WithdrawAmount, 0.5)
                                                           .Create();
            string json = JsonConvert.SerializeObject(withdrawRequest);
            StringContent bodyRequest = new(json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}