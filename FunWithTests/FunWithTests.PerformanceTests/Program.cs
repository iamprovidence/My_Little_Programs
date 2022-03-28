using BenchmarkDotNet.Running;

namespace FunWithTests.PerformanceTests
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<OrderTypeTest>();
        }
    }
}
