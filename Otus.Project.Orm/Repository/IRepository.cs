using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.Orm.Repository
{
    public interface IRepository<T, TId> : IDisposable where T : class
    {
        void Add(T item);

        void Delete(T item);

        Task Delete(TId id);

        void Update(T item);

        Task<T> FindByID(TId id, CancellationToken ct);

        Task<T> FindByExpression(Expression<Func<T, bool>> predicate, CancellationToken ct);

        Task<List<T>> FindAllByExpression(Expression<Func<T, bool>> predicate, CancellationToken ct);

        IQueryable<T> FindAll();

        Task CommitChangesAsync(CancellationToken ct);
    }
}
