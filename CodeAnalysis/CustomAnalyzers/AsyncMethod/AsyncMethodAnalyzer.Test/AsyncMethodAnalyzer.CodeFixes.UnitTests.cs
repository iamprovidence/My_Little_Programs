using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = AsyncMethodAnalyzer.Test.Verifiers.CSharpCodeFixVerifier<
    AsyncMethodAnalyzer.AsyncMethodAnalyzer,
    AsyncMethodAnalyzer.AsyncMethodAnalyzerCodeFixProvider>;

namespace AsyncMethodAnalyzer.Test
{
    [TestClass]
    public class AsyncMethodAnalyzerCodeFixesUnitTest
    {
        [TestMethod]
        public async Task Async_suffix_should_be_removed()
        {
            var test = @"
            using System.Threading.Tasks;

            class MyTestClass
            {   
                [|public Task MethodAsync() { return Task.CompletedTask; }|]
            }";

            var fixtest = @"
            using System.Threading.Tasks;

            class MyTestClass
            {   
                public Task Method() { return Task.CompletedTask; }
            }";

            await VerifyCS.VerifyCodeFixAsync(test, fixtest);
        }

        [TestMethod]
        public async Task Method_should_be_renamed_in_referenced_classes()
        {
            var test = @"
            using System.Threading.Tasks;

            class MyTestClass
            {   
                [|public Task MethodAsync() 
                {
                    return Task.CompletedTask; 
                }|]
            }

            class B
            {   
                public Task M() 
                { 
                    return new MyTestClass().MethodAsync();
                }
            }";

            var fixtest = @"
            using System.Threading.Tasks;

            class MyTestClass
            {   
                public Task Method() 
                {
                    return Task.CompletedTask; 
                }
            }

            class B
            {   
                public Task M() 
                { 
                    return new MyTestClass().Method();
                }
            }";

            await VerifyCS.VerifyCodeFixAsync(test, fixtest);
        }
    }
}
