using System;
using System.Linq.Expressions;
using UnitOfWork.Abstractions.Entity;
using UnitOfWork.Abstractions.Specifications.Combining;

namespace UnitOfWork.Abstractions.Specifications
{
	public interface ISpecification<TEntity>
		where TEntity: class, IEntity
	{
		bool IsSatisfiedBy(TEntity entity)
		{
			return ToFunction().Invoke(entity);
		}
		Func<TEntity, bool> ToFunction()
		{
			return ToExpression().Compile();
		}
		Expression<Func<TEntity, bool>> ToExpression();

		#region Operators
		public static bool operator true(ISpecification<TEntity> spec)
		{
			return false;
		}

		public static bool operator false(ISpecification<TEntity> spec)
		{
			return false;
		}

		public static ISpecification<TEntity> operator !(ISpecification<TEntity> spec)
		{
			Expression<Func<TEntity, bool>>  notExpression = Expression.Lambda<Func<TEntity, bool>>(
				Expression.Not(spec.ToExpression().Body), 
					spec.ToExpression().Parameters);

			return new Specification<TEntity>(notExpression);
		}

		public static ISpecification<TEntity> operator &(ISpecification<TEntity> left, ISpecification<TEntity> right)
		{
			var leftExpr = left.ToExpression();
			var rightExpr = right.ToExpression();
			var leftParam = leftExpr.Parameters[0];
			var rightParam = rightExpr.Parameters[0];

			return new Specification<TEntity>(
				Expression.Lambda<Func<TEntity, bool>>(
					Expression.AndAlso(
						leftExpr.Body,
						new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
					leftParam));
		}

		public static ISpecification<TEntity> operator |(ISpecification<TEntity> left, ISpecification<TEntity> right)
		{
			var leftExpr = left.ToExpression();
			var rightExpr = right.ToExpression();
			var leftParam = leftExpr.Parameters[0];
			var rightParam = rightExpr.Parameters[0];

			return new Specification<TEntity>(
				Expression.Lambda<Func<TEntity, bool>>(
					Expression.OrElse(
						leftExpr.Body,
						new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
					leftParam));
		}
		#endregion
	}
}
