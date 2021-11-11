using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.Orm.Repository
{
    public class GenericRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly DbContext dbContext;

        private DbSet<TEntity> Entities
        {
            get { return dbContext.Set<TEntity>(); }
        }

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TEntity item)
        {
            Entities.Add(item);
        }

        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public async Task Delete(TId id)
        {
            TEntity entity = await FindByID(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Update(TEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public async Task<TEntity> FindByID(TId id, CancellationToken ct = default)
        {
            return await Entities.FindAsync(new object[] { id }, cancellationToken: ct);
        }

        public Task<TEntity> FindByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().SingleOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> FindAllByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().Where(predicate);
        }

        public IQueryable<TEntity> FindAll()
        {
            return Entities.AsQueryable();
        }

        public async Task CommitChangesAsync(CancellationToken ct = default)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync(ct))
            {
                await dbContext.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
