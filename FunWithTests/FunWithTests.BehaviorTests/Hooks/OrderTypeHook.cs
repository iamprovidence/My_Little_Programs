using TechTalk.SpecFlow;

namespace FunWithTests.BehaviorTests.Hooks
{
    [Binding]
    public class OrderTypeHook
    {
        [BeforeTestRun]
        public static void Log()
        {
            System.Console.WriteLine("GG");
        }
    }
}
