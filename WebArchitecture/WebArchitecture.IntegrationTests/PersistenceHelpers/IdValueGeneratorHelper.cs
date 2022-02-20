using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebArchitecture.IntegrationTests.PersistenceHelpers
{
	internal static class IdValueGeneratorHelper
	{
		private static object _lockObject = new object();

		private static Random _random = new Random();
		private static HashSet<int> _idPull = new HashSet<int>();

		public static void SetIdValueGenerator(DbContext context)
		{
			var cache = context.GetService<IValueGeneratorCache>();

			var keyProperties = context.Model.GetEntityTypes()
				.Select(e => e.FindPrimaryKey()?.Properties[0])
				.Where(p => p != null)
				.Where(p => p.ClrType == typeof(int))
				.Where(p => p.ValueGenerated == ValueGenerated.OnAdd);

			foreach (var keyProperty in keyProperties)
			{
				cache.GetOrAdd(
					keyProperty,
					keyProperty.DeclaringEntityType,
					(p, e) => new TestValueGenerator(p, e));
			}
		}

		private class TestValueGenerator : ValueGenerator<int>
		{
			private IProperty _property;
			private IEntityType _entityType;

			public TestValueGenerator(IProperty property, IEntityType entityType)
			{
				_property = property;
				_entityType = entityType;
			}

			public override bool GeneratesTemporaryValues => false;

			public override int Next(EntityEntry entry)
			{
				return GetNewId();
			}
			private int GetNewId()
			{
				int id;

				lock (_lockObject)
				{
					do
					{
						id = _random.Next();
					} while (_idPull.Contains(id));

					_idPull.Add(id);
				}

				return id;
			}
		}
	}
}
