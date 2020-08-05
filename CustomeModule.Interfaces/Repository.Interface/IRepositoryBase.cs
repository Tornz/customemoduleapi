using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace CustomeModule.Interfaces.Services.Interface
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        IEnumerable<T> SqlQuery(string sql, params object[] objects);

        T GetByID(object id);

        void Insert(T entity);

        void Delete(object id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);

        void Save();
    }
}
