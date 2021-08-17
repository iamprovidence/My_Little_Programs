using EfCore.MockLib.Ef;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.MockLib.Common
{
	public interface IDbContext
	{
		IQueryable<User> Users { get; }

		T Add<T>(T entity) where T : class;
		void AddRange<T>(IEnumerable<T> entities) where T : class;
		void Remove<T>(T entity) where T : class;
		Task<int> SaveChangesAsync();
	}
}