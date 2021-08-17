using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;

namespace EfCore.MockLib.Mock
{
	public static class MoqExtensions
	{
		public static Mock<IQueryable<TEntity>> BuildMock<TEntity>(this IQueryable<TEntity> data) where TEntity : class
		{
			var queryableMock = new Mock<IQueryable<TEntity>>();

			var enumerable = new TestAsyncEnumerable<TEntity>(data);

			queryableMock
				.As<IAsyncEnumerable<TEntity>>()
				.Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
				.Returns(enumerable.GetEnumerator);
			queryableMock.As<IQueryable<TEntity>>()
				.Setup(m => m.Provider)
				.Returns(enumerable);
			queryableMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.Expression)
				.Returns(data?.Expression);
			queryableMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.ElementType)
				.Returns(data?.ElementType);
			queryableMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.GetEnumerator())
				.Returns(data?.GetEnumerator());

			return queryableMock;
		}

		public static Mock<DbSet<TEntity>> BuildMockDbSet<TEntity>(this IQueryable<TEntity> data) where TEntity : class
		{
			var dbSetMock = new Mock<DbSet<TEntity>>();
			var enumerable = new TestAsyncEnumerable<TEntity>(data);

			dbSetMock
				.As<IAsyncEnumerable<TEntity>>()
				.Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
				.Returns(enumerable.GetEnumerator);
			dbSetMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.Provider)
				.Returns(enumerable);
			dbSetMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.Expression)
				.Returns(data?.Expression);
			dbSetMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.ElementType)
				.Returns(data?.ElementType);
			dbSetMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.GetEnumerator())
				.Returns(data?.GetEnumerator());

			return dbSetMock;
		}

		public static Mock<DbQuery<TEntity>> BuildMockDbQuery<TEntity>(this IQueryable<TEntity> data) where TEntity : class
		{
			var dbQueryMock = new Mock<DbQuery<TEntity>>();
			var enumerable = new TestAsyncEnumerable<TEntity>(data);

			dbQueryMock
				.As<IAsyncEnumerable<TEntity>>()
				.Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
				.Returns(enumerable.GetEnumerator);
			dbQueryMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.Provider)
				.Returns(enumerable);
			dbQueryMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.Expression)
				.Returns(data?.Expression);
			dbQueryMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.ElementType)
				.Returns(data?.ElementType);
			dbQueryMock
				.As<IQueryable<TEntity>>()
				.Setup(m => m.GetEnumerator())
				.Returns(data?.GetEnumerator());

			return dbQueryMock;
		}
	}
}
