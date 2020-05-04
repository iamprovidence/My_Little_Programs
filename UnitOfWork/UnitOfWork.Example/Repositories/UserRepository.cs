using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Repositories;
using UnitOfWork.Example.Domains;
using UnitOfWork.Example.Models;

namespace UnitOfWork.Example.Repositories
{
	public class UserRepository : SqlRepositoryBase<User>, IUserRepository
	{
		public UserRepository(DbContext dbContext) 
			: base(dbContext) { }

		public async Task<IReadOnlyCollection<User>> Find(UserQueryModel queryModel)
		{
			IQueryable<User> entities = _dbContext.Set<User>();

			if (!string.IsNullOrWhiteSpace(queryModel.Name))
			{
				entities = entities.Where(e => e.Name == queryModel.Name);
			}

			if (queryModel.Age.HasValue)
			{
				entities = entities.Where(e => e.Age == queryModel.Age);
			}

			return await entities.ToArrayAsync();
		}
	}
}
