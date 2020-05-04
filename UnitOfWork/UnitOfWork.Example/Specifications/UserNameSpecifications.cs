using System;
using System.Linq.Expressions;
using UnitOfWork.Abstractions.Specifications;
using UnitOfWork.Example.Domains;

namespace UnitOfWork.Example.Specifications
{
	public class UserNameSpecifications : Specification<User>
	{
		private readonly string _name;

		public UserNameSpecifications(string name)
		{
			_name = name;
		}

		public override Expression<Func<User, bool>> ToExpression()
		{
			return (user) => user.Name == _name;
		}
	}
}
