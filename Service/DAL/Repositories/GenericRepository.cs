namespace DAL.Repositories
{
    using DAL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ProgramDatabaseModel context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ProgramDatabaseModel context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
            => dbSet.Find(id);

        public virtual void Insert(TEntity entity)
            => dbSet.Add(entity);

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }
    }
}
