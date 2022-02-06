class AddMigrationCommand : ICommand
{
	private readonly string _migrationName;
	private readonly string _migrationFolder;

	public AddMigrationCommand(string migrationName, string migrationFolder)
	{
		_migrationName = migrationName;
		_migrationFolder = migrationFolder;
	}

	public void Execute()
	{
		var migrationFolderPath = Path.Join(Environment.CurrentDirectory, _migrationFolder);
		Directory.CreateDirectory(migrationFolderPath);

		var migrationVersion = DateTimeOffset.UtcNow.UtcTicks;
		var migrationName = $"{migrationVersion}_{_migrationName}";
		var migrationPath = Path.Join(migrationFolderPath, $"{migrationName}.cs");
		var migrationContent = GenerateMigrationContent(migrationVersion);

		File.WriteAllText(migrationPath, migrationContent);

		Console.WriteLine("Migration has been added");
	}

	private string GenerateMigrationContent(long migrationVersion)
	{
		return @$"using EFCore.MongoDB.Migrations;
using MongoDB.Driver;
using SimpleMigrations;
using System;

namespace {_migrationFolder.Replace('/', '.')}
{{
	[Migration({migrationVersion}, nameof({_migrationName}))]
	internal class {_migrationName} : MongoDbMigrationBase
	{{
		protected override void Up(IMongoDatabase database)
		{{
			throw new NotImplementedException();
		}}

		protected override void Down(IMongoDatabase database)
		{{
			throw new NotImplementedException();
		}}
	}}
}}

";
	}
}
