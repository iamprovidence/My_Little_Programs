using EfCore.MockLib.Common;
using EfCore.MockLib.Ef;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.MockLib
{
	public class Service
	{
		private readonly IDbContext _dbContext;

		public Service(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddUser(User user)
		{
			_dbContext.Add(user);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<IReadOnlyCollection<User>> GetAdultUsers()
		{
			return await _dbContext
				.Users
				.Where(u => u.Age > 18)
				.ToListAsync();
		}

		public async Task<IReadOnlyCollection<User>> SearchUsers(string searchCondition)
		{
			return await _dbContext
				.Users
				.Where(u =>
					EF.Functions.Contains(u.Name, searchCondition) ||
					EF.Functions.Contains(u.Age.ToString(), searchCondition))
				.ToListAsync();
		}
	}
}
