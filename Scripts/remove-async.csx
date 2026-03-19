#!/usr/bin/env dotnet-script
// KNOWN LIMITATIONS:
// - Slow: loads the entire solution, then for each method builds/updates references one at a time.
// - Could be parallelized but isn't — sorry ^^
// - Does NOT rename files or classes that have "Async" in their name.
// - Does NOT rename properties that have "Async" in their name.
// - Does NOT rename regions, comments, or string literals that reference "Async" method names.
// - If both sync and async versions exist, it may still attempt the rename, breaking the build and leading to inconsistent behavior afterward.
#r "nuget: Microsoft.Build.Locator, 1.7.8"
#r "nuget: Microsoft.CodeAnalysis.CSharp, 5.3.0"
#r "nuget: Microsoft.CodeAnalysis.CSharp.Workspaces, 5.3.0"
#r "nuget: Microsoft.CodeAnalysis.Workspaces.MSBuild, 5.3.0"

using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Rename;

MSBuildLocator.RegisterDefaults();

_ = typeof(CSharpFormattingOptions); // force Microsoft.CodeAnalysis.CSharp.Workspaces to load
using var workspace = MSBuildWorkspace.Create(new Dictionary<string, string>
{
    { "SkipUnrecognizedProjects", "true" }
});
workspace.WorkspaceFailed += (_, e) =>
{
    Console.Error.WriteLine(e.Diagnostic.Message);
};

Console.Write("Enter solution (.sln) file path: ");
var slnPath = Console.ReadLine()?.Trim().Trim('"') ?? "";
if (!File.Exists(slnPath))
{
    Console.Error.WriteLine($"File not found: {slnPath}");
    return;
}

Console.Write("Log verbosity: 0=Quiet, 1=Normal, 2=Verbose [default: 1]: ");
var verbosity = int.TryParse(Console.ReadLine()?.Trim(), out var v) ? Math.Clamp(v, 0, 2) : 1;

void Log(int level, string msg)
{
    if (verbosity >= level) Console.WriteLine(msg);
}

var solution = await workspace.OpenSolutionAsync(slnPath);
Log(1, $"Loaded {solution.ProjectIds.Count} projects.");

foreach (var projectId in solution.ProjectIds)
{
    Log(1, $"Start {projectId.ToString()}");

    var project = solution.GetProject(projectId)!;
    Log(1, $"Processing {project.DocumentIds.Count} documents in '{project.Name}'");

    foreach (var documentId in project.DocumentIds)
    {
        var document = solution.GetDocument(documentId)!;
        Log(2, $"  [{document.FilePath}]");

        var semanticModel = await document.GetSemanticModelAsync();
        var root = await document.GetSyntaxRootAsync();
        if (semanticModel == null || root == null)
        {
            Log(2, $"    Skipped (no semantic model or syntax root)");
            continue;
        }

        var methodDeclarations = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(m => m.Identifier.Text.EndsWith("Async") ||  // method
                m.Identifier.Text.Contains("Async_")) // unit tests
            .ToList();

        if (methodDeclarations.Count == 0)
        {
            Log(2, $"    No Async methods found");
            continue;
        }

        foreach (var method in methodDeclarations)
        {
            if (semanticModel.GetDeclaredSymbol(method) is not IMethodSymbol symbol)
            {
                Log(2, $"    Skipped '{method.Identifier.Text}' (no symbol)");
                continue;
            }
            if (symbol.IsExtern || symbol.IsOverride)
            {
                Log(2, $"    Skipped '{method.Identifier.Text}' (extern or override)");
                continue;
            }
            if (symbol.ExplicitInterfaceImplementations.Any())
            {
                Log(2, $"    Skipped '{method.Identifier.Text}' (explicit interface implementation)");
                continue;
            }

            var containingType = symbol.ContainingType;
            bool implementsInterface = containingType.AllInterfaces
                .SelectMany(i => i.GetMembers().OfType<IMethodSymbol>())
                .Any(m => SymbolEqualityComparer.Default.Equals(
                    containingType.FindImplementationForInterfaceMember(m), symbol));
            if (implementsInterface)
            {
                Log(2, $"    Skipped '{method.Identifier.Text}' (implements interface)");
                continue;
            }

            var newName = method.Identifier.Text.Replace("Async", "");
            Log(0, $"    Renaming '{method.Identifier.Text}' -> '{newName}'");
            solution = await Renamer.RenameSymbolAsync(solution, symbol, new SymbolRenameOptions(), newName);
        }

        if (workspace.TryApplyChanges(solution))
        {
            Log(1, $"    Applied changes");
            solution = workspace.CurrentSolution;
        }
        else
        {
            Console.Error.WriteLine($"    Failed to apply changes");
        }
    }
    Log(1, $"Finish '{solution.GetProject(projectId)!.Name}'");
}
