using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Mocha
{

    [Generator]
    public class Class1 : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            // Build up the source code
            string stub = $@" // Auto-generated code
using System;

namespace Mocha
{{
    public static class Stub
    {{
        public static void Hello() 
        {{
        }}
    }}
}}
";
            var classes = context.Compilation.SyntaxTrees.SelectMany(x => x.GetRoot().DescendantNodes()).OfType<InterfaceDeclarationSyntax>();

            if (classes.Any())
            {
                var source = new StringBuilder();
                source.AppendLine("// Auto-generated code");
                source.AppendLine("using System;");
                source.AppendLine("");
                source.AppendLine("namespace Mocha");
                source.AppendLine("{");

                foreach (var classo in classes)
                {
                    source.AppendLine($@"
    public static class {classo.Identifier.Text}Stub
    {{
        public static void Hello() 
        {{
        }}
    }}
");
                }

                source.AppendLine("}");

                var sourceRes = source.ToString();

                context.AddSource($"MyClass.g.cs", SourceText.From(sourceRes, Encoding.UTF8));
            }
            else
            {
                context.AddSource($"MyClass.g.cs", SourceText.From(stub, Encoding.UTF8));
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
