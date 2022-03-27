namespace FunWithTests.IntegrationTests.Helpers
{
    using Xunit;

    public class FactSwitch : FactAttribute
    {
        // Comment out to enable tests
        public override string Skip => "Integration tests require services to be running in background";
    }
}
