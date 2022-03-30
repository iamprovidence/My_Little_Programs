namespace Mocha.SourceTextBuilders
{
    internal static class MyMockSourceTextBuilder
    {
        public static string GetContent() =>
    @"using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mocha
{
    public class MyMock<TMockable>
        where TMockable : class
    {
        private readonly Dictionary<string, Func<object>> _methodInterceptors = new Dictionary<string, Func<object>>();

        public MyMock<TMockable> Mock<TResult>(Expression<Func<TMockable, TResult>> methodCall, TResult result)
        {
            var methodExpression = (methodCall.Body as MethodCallExpression);
            var methodName = methodExpression.Method.Name;

            _methodInterceptors[methodName] = () => result;

            return this;
        }

        public TMockable GetObject()
        {
            var proxyType = Type.GetType($""Mocha.{typeof(TMockable).Name}Proxy"");

            return (TMockable)Activator.CreateInstance(proxyType, _methodInterceptors);
        }
    }
}
";
    }
}
