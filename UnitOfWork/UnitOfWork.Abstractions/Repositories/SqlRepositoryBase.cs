using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Entity;
using UnitOfWork.Abstractions.Specifications;

namespace UnitOfWork.Abstractions.Repositories
{
	public /*abstract*/ class SqlRepositoryBase<TEntity> : IRepository<TEntity>
		where TEntity : class, IEntity
	{
		protected readonly DbContext _dbContext;

		public SqlRepositoryBase(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int Count => _dbContext.Set<TEntity>().Count();
		public bool IsSynchronized => false;
		public object SyncRoot => new object();
		public bool IsReadOnly => false;

		public ValueTask<TEntity> FindAsync(params object[] keys)
		{
			return _dbContext.Set<TEntity>().FindAsync(keys);
		}

		public async Task<IReadOnlyCollection<TEntity>> Find(ISpecification<TEntity> specification)
		{
			return await _dbContext.Set<TEntity>().Where(specification.ToExpression()).ToArrayAsync();
		}

		public void Add(TEntity item)
		{
			_dbContext.Set<TEntity>().Add(item);
		}

		public async ValueTask AddAsync(TEntity item)
		{
			await _dbContext.Set<TEntity>().AddAsync(item);
		}

		public async ValueTask AddRangeAsync(IEnumerable<TEntity> item)
		{
			await _dbContext.Set<TEntity>().AddRangeAsync(item);
		}

		public void Clear()
		{
			IQueryable<TEntity> entities = _dbContext.Set<TEntity>();
			_dbContext.Set<TEntity>().RemoveRange(entities);
		}

		public bool Contains(TEntity item)
		{
			return _dbContext.Set<TEntity>().Contains(item);
		}

		public bool Remove(TEntity item)
		{
			_dbContext.Set<TEntity>().Remove(item);
			return true;
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection<TEntity>)this).CopyTo(array as TEntity[], index);
		}

		public void CopyTo(TEntity[] array, int arrayIndex)
		{
			foreach (TEntity entity in this)
			{
				array[arrayIndex++] = entity;
			}
		}
		public IEnumerator<TEntity> GetEnumerator()
		{
			return _dbContext.Set<TEntity>().AsEnumerable().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
