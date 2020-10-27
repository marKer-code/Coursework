namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        void Update(TEntity entityToUpdate);
        void Insert(TEntity entity);
        void Delete(TEntity entityToDelete);
        void Delete(int id);
        bool CheckUser(string login, string password);
    }
}
