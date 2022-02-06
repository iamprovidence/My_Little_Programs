static class CommandFactory
{
	public static ICommand Create(string[] args)
	{
		return args.FirstOrDefault() switch
		{
			"" or "--help" => new HelpCommand(),
			"--add-migration" => new AddMigrationCommand(migrationName: args[1], migrationFolder: args[2]),
			"--update-database" => new UpdateDatabaseCommand(),
			_ => new UndefinedCommand(args.FirstOrDefault()),
		};
	}
}
