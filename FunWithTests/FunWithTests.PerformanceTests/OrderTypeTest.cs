using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Domain;

namespace FunWithTests.PerformanceTests
{
    [RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class OrderTypeTest
    {
        [Benchmark]
        public void FromName()
        {
            OrderType.FromName("RegularOrder");
        }

        [Benchmark]
        public void FromValue()
        {
            OrderType.FromValue(1);
        }
    }
}
