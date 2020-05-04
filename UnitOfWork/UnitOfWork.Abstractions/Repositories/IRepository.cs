using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Entity;
using UnitOfWork.Abstractions.Specifications;

namespace UnitOfWork.Abstractions.Repositories
{
	public interface IRepository : ICollection
	{

	}

	public interface IRepository<TEntity> : IRepository, ICollection<TEntity> 
		where TEntity: class, IEntity
	{
		ValueTask<TEntity> FindAsync(params object[] keys);

		Task<IReadOnlyCollection<TEntity>> Find(ISpecification<TEntity> specification);

		ValueTask AddAsync(TEntity item);
		ValueTask AddRangeAsync(IEnumerable<TEntity> item);
	}
}
