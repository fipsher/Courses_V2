using Core.Interfaces;
using LightCaseClient;

namespace Data.Repositories
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        protected string BaseUrl { get; private set; }
        public Repository()
        {
            BaseUrl = "" + typeof(TEntity).Name;//should inject some class with strings
        }

        public virtual void Add(TEntity entity) => GenericProxies.RestPostNonQuery($"{BaseUrl}/Add", entity);
        public virtual void Delete(TId id) => GenericProxies.RestPostNonQuery($"{BaseUrl}/Delete", id);
        public virtual TEntity Find(TId id) => GenericProxies.RestGet<TEntity>($"{BaseUrl}/Find?id={id}");
        public virtual TEntity Get(int take, int skip) => GenericProxies.RestGet<TEntity>($"{BaseUrl}/Get?take={take}&ski{skip}");
        public virtual void Update(TEntity entity) => GenericProxies.RestPostNonQuery($"{BaseUrl}/Update", entity);
    }
}
