namespace Core.Interfaces
{
    public interface IRepository<TEntity, TId>
    {
        TEntity Find(TId id);

        void Add(TEntity entity);

        void Delete(TId id);

        //IEnumerable<TEntity> Where(); //TODO: Implement filter class

        void Update(TEntity entity);
    }
}
