using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace WriterTrainer.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
