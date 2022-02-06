$migrationName = Read-Host -Prompt 'Input your migration name'

efmongo --add-migration $migrationName 'Infrastructure/Migrations'