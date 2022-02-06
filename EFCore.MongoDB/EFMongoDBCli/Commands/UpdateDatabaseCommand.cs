using System.Reflection;

class UpdateDatabaseCommand : ICommand
{
	public void Execute()
	{
		BuildProject();

		var dbContext = GetMongoDbContext();
		dbContext.Migrate(GetMigrationAssembly());

		Console.WriteLine("Database has been updated");
	}

	private static void BuildProject()
	{
		var process = new Process()
		{
			StartInfo = new ProcessStartInfo
			{
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,

				FileName = "dotnet.exe",
				Arguments = $"build {Path.Join(Environment.CurrentDirectory, $"{GetProjectName()}.csproj")}",
			},
		};
		process.Start();
		process.WaitForExit();
	}

	private static MongoDbContext GetMongoDbContext()
	{
		var assembly = GetMigrationAssembly();

		var dbContextFactoryType = assembly
			.GetTypes()
			.Where(t => t.IsClass)
			.Where(t => !t.IsAbstract)
			.Where(t => t.GetInterfaces()
				.Where(i => i.IsGenericType)
				.Where(i => i.GetGenericTypeDefinition() == typeof(IDesignTimeDbContextFactory<>))
				.Any())
			.Single();

		var factory = Activator.CreateInstance(dbContextFactoryType) as IDesignTimeDbContextFactory<MongoDbContext>;

		return factory.CreateDbContext(Environment.GetCommandLineArgs());
	}

	private static Assembly GetMigrationAssembly()
	{
		var assemblyName = Directory.GetFiles(Environment.CurrentDirectory, $"{GetProjectName()}.dll", SearchOption.AllDirectories).First();

		return Assembly.LoadFrom(assemblyName);
	}

	private static string GetProjectName()
	{
		var projectFullPath = Directory.GetFiles(Environment.CurrentDirectory, "*.csproj", SearchOption.AllDirectories).Single();

		return Path.GetFileNameWithoutExtension(projectFullPath);
	}
}
