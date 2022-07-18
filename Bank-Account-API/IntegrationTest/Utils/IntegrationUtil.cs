namespace IntegrationTest.Utils
{
    public static class IntegrationUtil
    {
        public static HttpClient HttpFactory()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            return webAppFactory.CreateDefaultClient();
        }
    }
}