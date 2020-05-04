using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using UnitOfWork.Abstractions.Entity;
using UnitOfWork.Abstractions.Repositories;

namespace UnitOfWork.Abstractions.UnitOfWork
{
	public class SqlUnitOfWork : ISqlUnitOfWork, IDisposable
	{
		private readonly DbContext _dbContext;
		private IDictionary<Type, object> _repositories;

		public SqlUnitOfWork(DbContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new ConcurrentDictionary<Type, object>();
		}

		public TRepository Get<TRepository>()
		{
			Type key = typeof(TRepository);

			if (!_repositories.ContainsKey(key))
				throw new InvalidOperationException("Current repository has not been registered. Verify that interface and implementation both exist.");

			return (TRepository)_repositories[key];
		}

		public void RegisterRepositoriesFromAssembly(Assembly assembly)
		{
			IDictionary<Type, object> repositories = ScanAssembly(assembly);

			_repositories = new ConcurrentDictionary<Type, object>(repositories);
		}

		public void RegisterRepository<TInterface, TRepository>() where TRepository : TInterface
		{
			_repositories[typeof(TInterface)] = Activator.CreateInstance(typeof(TRepository), new object[] { _dbContext });
		}

		#region LoadRepositories
		private IDictionary<Type, object> ScanAssembly(Assembly assembly)
		{
			IEnumerable<Type> interfaces = LoadRepositoriesInterfaces(assembly);
			IEnumerable<Type> implementation = LoadRepositoriesImplementation(assembly);
			IDictionary<Type, object> repositories = JoinAbstractionAndImplementation(interfaces, implementation);

			return repositories;
		}

		private IEnumerable<Type> LoadRepositoriesInterfaces(Assembly assembly)
		{
			return
				from t in assembly.GetTypes()
				from ti in t.GetInterfaces()
				where t.IsInterface && ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IRepository<>)
				select t;
		}

		private IEnumerable<Type> LoadRepositoriesImplementation(Assembly assembly)
		{
			return
				from t in assembly.GetTypes()
				from ti in t.GetInterfaces()
				where t.IsClass && !t.IsAbstract && ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IRepository<>)
				select t;
		}

		private IDictionary<Type, object> JoinAbstractionAndImplementation(IEnumerable<Type> abstraction, IEnumerable<Type> implementation)
		{
			return
				(from impl in implementation
				 from i in impl.GetInterfaces()
				 join abst in abstraction on i equals abst
				 where abst.IsAssignableFrom(impl)
				 select new { abst, impl })
				.ToDictionary(k => k.abst, v => Activator.CreateInstance(v.impl, new object[] { _dbContext }));
		}
		#endregion

		public Task SaveChangesAsync()
		{
			return _dbContext.SaveChangesAsync();
		}

		public Task LoadCollection<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> include)
			where TEntity : class, IEntity
			where TProperty : class
		{
			return _dbContext.Entry(entity).Collection(include).LoadAsync();
		}

		public Task LoadReference<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> include)
			where TEntity : class, IEntity
			where TProperty : class
		{
			return _dbContext.Entry(entity).Reference(include).LoadAsync();
		}

		public void Update<TEntity>(TEntity entityToUpdate)
			where TEntity : class, IEntity
		{
			if (_dbContext.Entry(entityToUpdate).State == EntityState.Detached)
			{
				_dbContext.Set<TEntity>().Attach(entityToUpdate);
			}
			_dbContext.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public void Dispose()
		{
			_repositories.Clear();
			_dbContext.Dispose();
		}
	}
}
