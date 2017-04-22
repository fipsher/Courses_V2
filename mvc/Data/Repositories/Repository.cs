using Core.Helpers;
using Core.Interfaces;
using LightCaseClient;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected string BaseUrl { get; private set; }
        public Repository(string webApiUrl)
        {
            BaseUrl = webApiUrl + typeof(TEntity).Name;
        }

        public virtual void Add(TEntity entity) => GenericProxies.RestPostNonQuery($"{BaseUrl}/add", entity);
        public virtual void Delete(string id) => GenericProxies.RestPostNonQuery($"{BaseUrl}/delete", id);
        public virtual List<TEntity> Find(BaseSearchFilter<TEntity> filter) => GenericProxies.RestPost<List<TEntity>, BaseSearchFilter<TEntity>>($"{BaseUrl}/find", filter);
        public virtual TEntity GetAll() => GenericProxies.RestGet<TEntity>($"{BaseUrl}/get-all");
        public virtual void Update(TEntity entity) => GenericProxies.RestPostNonQuery($"{BaseUrl}/update", entity);
    }

}
