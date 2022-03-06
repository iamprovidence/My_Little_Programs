$analyzerProjects = Get-ChildItem -Path '../CustomAnalyzers/*/*.CodeFixes/*.csproj'
$outputPath = "analyzers"

foreach ($project in $analyzerProjects)
{
	dotnet build $project
	
	Get-ChildItem -Path $outputPath -Exclude *.gitkeep | Foreach-Object {Remove-Item $_.FullName}

	$directory = Split-Path -parent $project
	Get-ChildItem -Path "$directory\*.dll" -Recurse | Copy-Item -Destination $outputPath
}
