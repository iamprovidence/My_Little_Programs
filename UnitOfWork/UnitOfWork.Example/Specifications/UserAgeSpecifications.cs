using System;
using System.Linq.Expressions;
using UnitOfWork.Abstractions.Specifications;
using UnitOfWork.Example.Domains;

namespace UnitOfWork.Example.Specifications
{
	public class UserAgeSpecifications : Specification<User>
	{
		private readonly int _age;

		public UserAgeSpecifications(int age)
		{
			_age = age;
		}

		public override Expression<Func<User, bool>> ToExpression()
		{
			return (user) => user.Age == _age;
		}
	}
}
