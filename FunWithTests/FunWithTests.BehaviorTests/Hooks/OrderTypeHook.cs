using BoDi;
using TechTalk.SpecFlow;

namespace FunWithTests.BehaviorTests.Hooks
{
    [Binding]
    public class OrderTypeHook
    {
        [BeforeScenario("OrderType")]
        public static void Log(IObjectContainer di)
        {
            System.Console.WriteLine("GG");
            di.RegisterInstanceAs(new object());
        }
    }
}
