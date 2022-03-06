using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AsyncMethodAnalyzer
{

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AsyncMethodAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AsyncMethodAnalyzer";

        private const string Title = "The method contains 'Async' suffix";
        private const string MessageFormat = "The method '{0}' contains 'Async' suffix";
        private const string Description = "Asynchronous method should not contain 'Async' suffix if there is no synchronous implementation";
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.RegisterSyntaxTreeAction(AnalyzeMethods);
        }

        private void AnalyzeMethods(SyntaxTreeAnalysisContext context)
        {
            var root = context.Tree.GetRoot(context.CancellationToken);

            var methodNames = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Select(x => x.Identifier.Text)
                .ToImmutableHashSet();

            foreach (var statement in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var methodName = statement.Identifier.Text;

                if (!methodName.Contains("Async"))
                {
                    continue;
                }

                var syncMethodName = methodName.Substring(0, methodName.LastIndexOf("Async"));

                if (methodNames.Contains(syncMethodName))
                {
                    continue;
                }

                context.ReportDiagnostic(Diagnostic.Create(Rule, statement.GetLocation(), methodName));
            }
        }
    }
}
