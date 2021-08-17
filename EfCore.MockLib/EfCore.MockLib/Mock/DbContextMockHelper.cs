using EfCore.MockLib.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfCore.MockLib.Mock
{
	public static class DbContextMockHelper
	{
		public static Mock<IDbContext> BuildDbContextMock()
		{
			return new Mock<IDbContext>();
		}
	}

	public static class DbContextMockExtensions
	{
		public static Mock<IDbContext> SetupAddCallback<T>(this Mock<IDbContext> dbContextMock, Action<T> callback)
			where T : class
		{
			dbContextMock
				.Setup(dbc => dbc.Add(It.IsAny<T>()))
				.Callback(callback);

			return dbContextMock;

		}

		public static Mock<IDbContext> SetupAddRangeCallback<T>(this Mock<IDbContext> dbContextMock, Action<IEnumerable<T>> onAddCallback)
			where T : class
		{
			dbContextMock
				.Setup(dbc => dbc.AddRange(It.IsAny<IEnumerable<T>>()))
				.Callback(onAddCallback);

			return dbContextMock;
		}

		public static Mock<IDbContext> SetupRemoveCallback<T>(this Mock<IDbContext> dbContextMock, Action<T> onRemoveCallback)
			where T : class
		{
			dbContextMock
				.Setup(dbc => dbc.Remove(It.IsAny<T>()))
				.Callback(onRemoveCallback);

			return dbContextMock;
		}

		public static Mock<IDbContext> MockDbSet<T>(
			this Mock<IDbContext> dbContextMock,
			Expression<Func<IDbContext, IQueryable<T>>> action,
			ICollection<T> collection) where T : class
		{
			dbContextMock
				.Setup(action)
				.Returns(GetQueryableMockDbSet(collection));

			return dbContextMock;
		}

		public static Mock<IDbContext> MockDbSetAsEmpty<T>(
			this Mock<IDbContext> dbContextMock,
			Expression<Func<IDbContext, IQueryable<T>>> action) where T : class
		{
			dbContextMock
				.MockDbSet(action, Enumerable.Empty<T>().ToList());

			return dbContextMock;
		}

		public static Mock<IDbContext> SetupSaveCallback(this Mock<IDbContext> dbContextMock, Action onSaveCallback)
		{
			dbContextMock
				.Setup(dbc => dbc.SaveChangesAsync())
				.Callback(onSaveCallback)
				.Returns(Task.FromResult(1));

			return dbContextMock;
		}

		private static IQueryable<T> GetQueryableMockDbSet<T>(ICollection<T> items) where T : class
		{
			return items.AsQueryable().BuildMock().Object;
		}
	}
}
