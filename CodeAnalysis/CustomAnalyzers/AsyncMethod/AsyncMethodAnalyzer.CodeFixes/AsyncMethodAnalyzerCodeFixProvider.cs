namespace AsyncMethodAnalyzer
{
    using System.Collections.Immutable;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Rename;

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AsyncMethodAnalyzerCodeFixProvider)), Shared]
    public class AsyncMethodAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string Title = "Remove 'Async' suffix";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get 
            { 
                return ImmutableArray.Create(AsyncMethodAnalyzer.DiagnosticId); 
            }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var methodDeclaration = root.FindToken(diagnosticSpan.Start)
                .Parent
                .AncestorsAndSelf()
                .OfType<MethodDeclarationSyntax>()
                .First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Title,
                    createChangedSolution: c => RemoveAsyncSuffix(context.Document, methodDeclaration, c),
                    equivalenceKey: Title),
                diagnostic);
        }

        private async Task<Solution> RemoveAsyncSuffix(Document document, MethodDeclarationSyntax method, CancellationToken cancellationToken)
        {
            var identifierToken = method.Identifier;
            var newName = identifierToken.Text.Substring(0, identifierToken.Text.LastIndexOf("Async"));

            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var methodSymbol = semanticModel.GetDeclaredSymbol(method, cancellationToken);

            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;
            var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, methodSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

            return newSolution;
        }
    }
}
