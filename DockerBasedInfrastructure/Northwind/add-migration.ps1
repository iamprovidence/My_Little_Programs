$migrationName = Read-Host -Prompt 'Input your migration name'

dotnet ef migrations add $migrationName --startup-project ../Northwind.Api/Northwind.Api.csproj --context NorthwindDbContext --output-dir Infrastructure/Persistence/Migrations
