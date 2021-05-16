$migrationName = Read-Host -Prompt 'Input your migration name'

dotnet ef migrations add $migrationName --startup-project ../../Project.csproj --context DbContext
