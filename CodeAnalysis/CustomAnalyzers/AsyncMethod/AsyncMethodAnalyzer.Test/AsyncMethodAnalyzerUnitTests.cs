using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = AsyncMethodAnalyzer.Test.Verifiers.CSharpCodeFixVerifier<
    AsyncMethodAnalyzer.AsyncMethodAnalyzer,
    AsyncMethodAnalyzer.AsyncMethodAnalyzerCodeFixProvider>;

namespace AsyncMethodAnalyzer.Test
{
    [TestClass]
    public class AsyncMethodAnalyzerUnitTest
    {
        [TestMethod]
        public async Task Sync_method_should_be_valid()
        {
            var test = @"
            class MyTestClass
            {   
                public void Method() { }
            }";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }


        [TestMethod]
        public async Task Async_method_with_suffix_should_be_valid_when_sync_version_present()
        {
            var test = @"
            using System.Threading.Tasks;

            class MyTestClass
            {   
                public void Method() { }
                public Task MethodAsync() { return Task.CompletedTask; }
            }";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task Async_method_should_not_have_suffix()
        {
            var test = @"
            using System.Threading.Tasks;

            class MyTestClass
            {   
                {|#0:public Task MethodAsync() { return Task.CompletedTask; }|}
            }";

            var expectedErrors = VerifyCS.Diagnostic("AsyncMethodAnalyzer")
                .WithLocation(0)
                .WithArguments("MethodAsync")
                .WithSeverity(DiagnosticSeverity.Error);

            await VerifyCS.VerifyAnalyzerAsync(test, expectedErrors);
        }
    }
}
