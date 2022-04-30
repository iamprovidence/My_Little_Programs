$migrationName = Read-Host -Prompt 'Input your migration name'

# use time span to avoid collision?
$latestMigrationVersion = Get-ChildItem 'Infrastrucutre\Migrations' | 
    Select-Object -ExpandProperty 'Name' |
    ForEach-Object { $_.Split("_")[0] } | 
    Sort-Object |
    Select-Object -Last 1

$migrationVersion = ([int]$latestMigrationVersion + 1).ToString().PadLeft(3, '0')

# create migration
$migrationPath = "Infrastrucutre\Migrations\$migrationVersion`_$migrationName.cs"
$migrationContent = "using Client.Domain;
using EFCore.MongoDB.Migrations;
using MongoDB.Driver;
using SimpleMigrations;

namespace Client.Infrastructure.Migrations
{
    using System;
    using SimpleMigrations;

    [Migration($(+$migrationVersion), `"$migrationName`")]
    public class $migrationName : MongoDbMigrationBase
    {
        protected override void Up()
        {
            throw new NotImplementedException(`"Down migrations are not supported`");
        }

        protected override void Down()
        {
            throw new NotImplementedException(`"Down migrations are not supported`");
        }
    }
}"

New-Item $migrationPath | Out-Null
Set-Content $migrationPath $migrationContent