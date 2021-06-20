using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Persistence.Abstractions
{
	public interface IApplicationDbContext
	{
		DbSet<T> GetDbSet<T>()
			where T : class; // where IAggregateRoot 

		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
