namespace DAL.Repositories
{
    using DAL.Entities;
    using DAL.Interfaces;
    using System.Data.Entity;
    using System.Linq;

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

        public bool CheckUser(string login, string password)
            => context.Users.FirstOrDefault
                (u => u.Login == login && u.HashPassword == password) == null ?
                false : true;
        public bool CheckLogin(string login)
            => context.Users.FirstOrDefault
                (u => u.Login == login) == null ?
                false : true;

        public int GetUserId(string login)
            => context.Users.First(u => u.Login == login).Id;

        public UserInfo GetUserInfoByLogin(string login)
            => context.UserInfos.First(u => u.UserId == GetUserId(login));
        
    }
}
