using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Mocha.SourceTextBuilders;

namespace Mocha
{
    [Generator]
    public class MochaSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource($"MyMock.g.cs", SourceText.From(MyMockSourceTextBuilder.GetContent(), Encoding.UTF8));

            var interfaces = context
                .Compilation
                .SyntaxTrees
                .SelectMany(x => x.GetRoot().DescendantNodes())
                .OfType<InterfaceDeclarationSyntax>()
                .ToList();

            foreach (var interfaceDeclaration in interfaces)
            {
                var proxyContent = ProxySourceTextBuilder.GetContent(interfaceDeclaration);
                context.AddSource($"{interfaceDeclaration.Identifier.Text}Proxy.g.cs", SourceText.From(proxyContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG && false
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Launch();
            }
#endif
        }
    }
}
