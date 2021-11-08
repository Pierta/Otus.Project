using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        public void Delete(TId id)
        {
            TEntity entity = FindByID(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Update(TEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public TEntity FindByID(TId id)
        {
            return Entities.Find(id);
        }

        public TEntity FindByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> FindAllByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().Where(predicate);
        }

        public IQueryable<TEntity> FindAll()
        {
            return Entities.AsQueryable();
        }

        public void CommitChanges()
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
