namespace FunWithTests.IntegrationTests.Helpers
{
    public class FunWithTestsHttpClient : WebApiHttpClient
    {
        public static string ServerUrl = "https://localhost:6001";

        public FunWithTestsHttpClient()
            : base(ServerUrl)
        {
        }
    }
}
