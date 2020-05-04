using System.Reflection;

namespace UnitOfWork.Abstractions.UnitOfWork
{
	public interface IUnitOfWork
	{
		public TRepository Get<TRepository>();
		public void RegisterRepositoriesFromAssembly(Assembly assembly);
		public void RegisterRepository<TInterface, TRepository>() where TRepository : TInterface;
	}
}
