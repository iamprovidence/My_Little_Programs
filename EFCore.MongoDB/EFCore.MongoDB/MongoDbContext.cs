using EFCore.MongoDB.Migrations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimpleMigrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace EFCore.MongoDB
{
	public abstract class MongoDbContext
	{
		private readonly MongoDbConfiguration _configuration;
		private readonly IMongoDatabase _database;

		public MongoDbContext(IOptions<MongoDbConfiguration> options)
		{
			_configuration = options.Value;
			_database = _configuration.Open();
		}

		public IMongoCollection<TEntity> GetCollection<TEntity>()
			where TEntity : class
		{
			var collectionName = typeof(TEntity).GetCustomAttribute<TableAttribute>().Name;

			return _database.GetCollection<TEntity>(collectionName);
		}

		public void Migrate(Assembly migrationsAssembly)
		{
			var databaseProvider = new MongoDbDatabaseProvider(_configuration, _database);

			var migrator = new SimpleMigrator<MongoDbConfiguration, MongoDbMigrationBase>(migrationsAssembly, databaseProvider);

			migrator.Load();
			migrator.MigrateToLatest();
		}

		protected abstract void OnModelCreating();

		protected static void ApplyConfigurationsFromAssembly(Assembly assembly)
		{
			foreach (var configuration in LoadConfigurationsFromAssembly(assembly))
			{
				Activator.CreateInstance(configuration);
			}
		}

		private static IEnumerable<Type> LoadConfigurationsFromAssembly(Assembly assembly)
		{
			return assembly
				.GetTypes()
				.Where(t => t.IsClass)
				.Where(t => !t.IsAbstract)
				.Where(t => t.BaseType != null)
				.Where(t => t.BaseType.IsGenericType)
				.Where(t => t.BaseType.GetGenericTypeDefinition() == typeof(EntityConfigurationBase<>));
		}

	}
}
