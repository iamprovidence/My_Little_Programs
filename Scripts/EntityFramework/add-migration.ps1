$migrationName = Read-Host -Prompt 'Input your migration name'
$outputFolder = 'Migrations'

dotnet ef migrations add $migrationName `
	--startup-project ./Project.csproj ` # project with IDesignTimeDbContextFactory, DI, etc
	--project ./Project.csproj `
	--context DbContext `
	--output-dir $outputFolder 

$fileName = (Get-Item -Path "$outputFolder\*_$migrationName.cs").Name

dotnet format --include "$outputFolder\$fileName" --fix-analyzers warn
