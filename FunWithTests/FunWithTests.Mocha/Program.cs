using System;

namespace FunWithTests.Mocha
{
    public interface IService
    {
        string Hello();
        string World();
    }

    partial class Program
    {
        static void Main(string[] args)
        {
            var serviceMock = new global::Mocha.MyMock<IService>();

            serviceMock
                .Mock(m => m.Hello(), "Hello")
                .Mock(m => m.World(), "World");

            var service = serviceMock.GetObject();

            Console.WriteLine($"{service.Hello()} {service.World()}");
        }
    }
}