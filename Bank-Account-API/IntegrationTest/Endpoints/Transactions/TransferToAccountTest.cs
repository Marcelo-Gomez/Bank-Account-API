namespace IntegrationTest.Endpoints.Transactions
{
    public class TransferToAccountEndpointTest
    {
        private readonly string endpointPath = "/Api/Bank/Transaction/TransferToAccount";

        [Test]
        public async Task TransferToAccount_WithValidTransferAccountRequest_ResponseIsOK()
        {
            // Arrange
            TransferAccountRequest transferAccountRequest = new Fixture().Build<TransferAccountRequest>()
                                                                         .With(p => p.AccountId, new Guid("7de32560-8059-4085-9219-c10c069ad9fe"))
                                                                         .With(p => p.AccountReceiveId, new Guid("1c13d24a-2c5f-47c3-9245-0d1986e55cca"))
                                                                         .With(p => p.TransferAmount, 1.5)
                                                                         .Create();
            string json = JsonConvert.SerializeObject(transferAccountRequest);
            StringContent bodyRequest = new (json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task TransferToAccount_WithInvalidAccountId_ResponseIsBadRequest()
        {
            // Arrange
            TransferAccountRequest transferAccountRequest = new Fixture().Build<TransferAccountRequest>()
                                                                         .With(p => p.TransferAmount, 1.5)
                                                                         .Create();
            string json = JsonConvert.SerializeObject(transferAccountRequest);
            StringContent bodyRequest = new(json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TransferToAccount_WithInvalidTransferAmount_ResponseIsBadRequest()
        {
            // Arrange
            TransferAccountRequest transferAccountRequest = new Fixture().Build<TransferAccountRequest>()
                                                                         .With(p => p.AccountId, new Guid("7de32560-8059-4085-9219-c10c069ad9fe"))
                                                                         .With(p => p.AccountReceiveId, new Guid("1c13d24a-2c5f-47c3-9245-0d1986e55cca"))
                                                                         .With(p => p.TransferAmount, 0.5)
                                                                         .Create();
            string json = JsonConvert.SerializeObject(transferAccountRequest);
            StringContent bodyRequest = new(json, Encoding.UTF8, "application/json");

            // Act
            var response = await IntegrationUtil.HttpFactory().PostAsync(endpointPath, bodyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}