using System;
using System.Linq;
using System.Linq.Expressions;

namespace Otus.Project.Orm.Repository
{
    public interface IRepository<T, TId> : IDisposable where T : class
    {
        void Add(T item);

        void Delete(T item);

        void Delete(TId id);

        void Update(T item);

        T FindByID(TId id);

        T FindByExpression(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindAllByExpression(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindAll();

        void CommitChanges();
    }
}
