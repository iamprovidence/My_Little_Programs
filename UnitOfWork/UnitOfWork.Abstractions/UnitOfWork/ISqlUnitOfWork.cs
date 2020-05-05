using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Entity;

namespace UnitOfWork.Abstractions.UnitOfWork
{
	public interface ISqlUnitOfWork : IUnitOfWork, IDisposable
	{
		Task SaveChangesAsync();

		void Update<TEntity>(TEntity entityToUpdate)
			where TEntity : class, IEntity;

		Task LoadAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> include)
			where TEntity : class, IEntity
			where TProperty : class, IEntity;

		Task LoadAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> include)
			where TEntity : class, IEntity
			where TProperty : class, IEntity;
	}
}
