using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Repositories;
using UnitOfWork.Example.Domains;

namespace UnitOfWork.Example.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<IReadOnlyCollection<User>> Find(Models.UserQueryModel queryModel);
	}
}
