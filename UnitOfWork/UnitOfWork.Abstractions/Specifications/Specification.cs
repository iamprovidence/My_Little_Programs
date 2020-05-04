using System;
using System.Linq.Expressions;
using UnitOfWork.Abstractions.Entity;

namespace UnitOfWork.Abstractions.Specifications
{
	public class Specification<TEntity> : ISpecification<TEntity>
		where TEntity : class, IEntity
	{
		protected Expression<Func<TEntity, bool>> Predicate;

		protected Specification() { }

		public Specification(Expression<Func<TEntity, bool>> predicate)
		{
			Predicate = predicate;
		}

		public virtual Expression<Func<TEntity, bool>> ToExpression()
		{
			return Predicate;
		}
	}
}
