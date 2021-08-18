using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EfCore.MockLib.Mock
{
	internal class TestExpressionVisitor : ExpressionVisitor
	{
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			// replace
			// x => EF.Functions.Contains(x.Property, searchCondition)
			// with
			// x => x.Property.Contains(searchCondition, StringComparison.InvariantCultureIgnoreCase)
			if (IsSqlServerContainsExpression(node))
			{
				var (efFunctions, propertyInstance, searchCondition) = node.Arguments;

				return GetStringContainsExpression(propertyInstance, searchCondition);
			}

			return base.VisitMethodCall(node);
		}

		private bool IsSqlServerContainsExpression(MethodCallExpression node)
		{
			var sqlServerContainsMethodInfo = typeof(SqlServerDbFunctionsExtensions)
				.GetMethod(nameof(SqlServerDbFunctionsExtensions.Contains), new[] { typeof(DbFunctions), typeof(string), typeof(string) });

			return node.Method == sqlServerContainsMethodInfo;
		}

		private Expression GetStringContainsExpression(Expression instance, Expression searchCondition)
		{
			var containsMethodInfo = typeof(string)
				.GetMethod(nameof(string.Contains), new[] { typeof(string), typeof(StringComparison) });
			var instanceExpression = Expression.Coalesce(instance, Expression.Constant(string.Empty));
 
			// (instance ?? string.Empty).Contains(searchCondition, StringComparison.InvariantCultureIgnoreCase)  		
			return Expression.Call(instanceExpression, containsMethodInfo, searchCondition, Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
