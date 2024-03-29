﻿using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EfCore.MockLib.Mock
{
	internal class TestAsyncEnumerable<T> : IAsyncEnumerable<T>, IOrderedQueryable<T>, IAsyncQueryProvider
	{
		private IEnumerable<T> _enumerable;

		public TestAsyncEnumerable(Expression expression)
		{
			Expression = expression;
		}

		public TestAsyncEnumerable(IEnumerable<T> enumerable)
		{
			_enumerable = enumerable;
		}

		public IAsyncEnumerator<T> GetEnumerator()
		{
			return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
		}

		public IQueryable CreateQuery(Expression expression)
		{
			if (expression is MethodCallExpression m)
			{
				var resultType = m.Method.ReturnType; // it should be IQueryable<T>
				var tElement = resultType.GetGenericArguments().First();
				var queryType = typeof(TestAsyncEnumerable<>).MakeGenericType(tElement);
				return (IQueryable)Activator.CreateInstance(queryType, expression);
			}
			return new TestAsyncEnumerable<T>(expression);
		}

		public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
		{
			return new TestAsyncEnumerable<TEntity>(expression);
		}

		public object Execute(Expression expression)
		{
			return CompileExpressionItem<object>(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
			return CompileExpressionItem<TResult>(expression);
		}

		public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
		{
			return new TestAsyncEnumerable<TResult>(expression);
		}

		TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			var expectedResultType = typeof(TResult).GetGenericArguments()[0];
			var executionResult = typeof(IQueryProvider)
								 .GetMethod(
									  name: nameof(IQueryProvider.Execute),
									  genericParameterCount: 1,
									  types: new[] { typeof(Expression) })
								 .MakeGenericMethod(expectedResultType)
								 .Invoke(this, new[] { expression });

			return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
										?.MakeGenericMethod(expectedResultType)
										 .Invoke(null, new[] { executionResult });
		}

		public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
		{
			if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);

			return _enumerable.ToAsyncEnumerable().GetAsyncEnumerator();
		}

		public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(CompileExpressionItem<TResult>(expression));
		}


		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);

			return _enumerable.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);

			return _enumerable.GetEnumerator();
		}

		public Type ElementType => typeof(T);

		public Expression Expression { get; }

		public IQueryProvider Provider => this;

		private static TResult CompileExpressionItem<TResult>(Expression expression)
		{
			var rewriter = new TestExpressionVisitor();
			var body = rewriter.Visit(expression);
			var lambda = Expression.Lambda<Func<TResult>>(body, (IEnumerable<ParameterExpression>)null);

			return lambda.Compile()();
		}
	}
}
