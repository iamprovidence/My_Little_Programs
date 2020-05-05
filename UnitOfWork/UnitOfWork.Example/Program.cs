using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Specifications;
using UnitOfWork.Abstractions.UnitOfWork;
using UnitOfWork.Example.Domains;
using UnitOfWork.Example.Infrastructure;
using UnitOfWork.Example.Models;
using UnitOfWork.Example.Repositories;
using UnitOfWork.Example.Specifications;

namespace UnitOfWork.Example
{
	class Program
	{
		static IEnumerable<User> GetUsers()
		{
			return new User[]
			{
				new User
				{
					Age = 10,
					Name = "U1",
				},
				new User
				{
					Age = 10,
					Name = "U2",
				},
				new User
				{
					Age = 20,
					Name = "U1",
				},
				new User
				{
					Age = 20,
					Name = "U2",
				},
			};
		}

		static void Print(IEnumerable<User> users)
		{
			foreach(User user in users)
			{
				Console.WriteLine(user.ToString());
			}
		}

		static async Task Main(string[] args)
		{
			using DbContext dbContext = new SqlDbContext();
			using ISqlUnitOfWork sqlUnitOfWork = new SqlUnitOfWork(dbContext);
			sqlUnitOfWork.RegisterRepositoriesFromAssembly(Assembly.GetExecutingAssembly());

			IEnumerable<User> users = GetUsers();

			await sqlUnitOfWork.Get<IUserRepository>().AddRangeAsync(users);
			await sqlUnitOfWork.SaveChangesAsync();

			Console.WriteLine("Query");
			UserQueryModel queryModel = new UserQueryModel
			{
				Age = 10,
				Name = "U1",
			};
			IEnumerable<User> queryResult = await sqlUnitOfWork.Get<IUserRepository>().Find(queryModel);
			Print(queryResult);

			Console.WriteLine("Specification");
			ISpecification<User> ageSpecification = new UserAgeSpecifications(age: 20);
			ISpecification<User> nameSpecification = new UserNameSpecifications(name: "U2");
			IEnumerable<User> specificationResult1 = await sqlUnitOfWork.Get<IUserRepository>().Find(ageSpecification && nameSpecification);
			Print(specificationResult1);
			Console.WriteLine();
			IEnumerable<User> specificationResult2 = await sqlUnitOfWork.Get<IUserRepository>().Find(ageSpecification || nameSpecification);
			Print(specificationResult2);

			Console.ReadLine();
		}
	}
}
