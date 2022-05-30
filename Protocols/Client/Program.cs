using System;
using System.Threading.Tasks;
using Client.GraphQL;
using Client.gRPC;
using Client.Http;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await ApiClientTest.Run<HttpApiClient>("Http");
            await ApiClientTest.Run<GraphQlApiClient>("GraphQl");
            await ApiClientTest.Run<GrpcApiClient>("gRPC");

            Console.ReadLine();
        }
    }
}
