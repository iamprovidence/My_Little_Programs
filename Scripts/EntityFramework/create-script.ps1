$dir = "./Migrations"
$latestFiles = Get-ChildItem -Path $dir\* -Exclude @("*.sql", "*.Designer.cs", "DbContextModelSnapshot.cs") | Sort-Object Name -Descending | Select-Object -First 2

$lastMigrationName = $latestFiles[0].Basename
$previousMigrationName = $latestFiles[1].Basename

Write-Output "From:"
$previousMigrationName
Write-Output "To:"
$lastMigrationName

Write-Output "Start"
dotnet ef migrations script $previousMigrationName $lastMigrationName --output $dir/$lastMigrationName.sql --startup-project ../../Project.csproj --context DbContext
