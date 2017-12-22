using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using LightCaseClient;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository, IRepository<TEntity> where TEntity : Entity, new()
    {
        public string BaseUrl { get; private set; }
        public string ApiUrl { get; private set; }
        public Repository(string webApiUrl)
        {
            ApiUrl = webApiUrl;
            BaseUrl = webApiUrl + typeof(TEntity).Name.ToLower();
        }

        public virtual void Add(TEntity entity) => GenericProxies.RestPut($"{BaseUrl}/add", entity);
        public virtual void Delete(string id) => GenericProxies.RestDelete($"{BaseUrl}/delete-by-id", new { id = id });
        public virtual List<TEntity> Find(SearchFilter<TEntity> filter) => GenericProxies.RestPost<List<TEntity>, SearchFilter<TEntity>>($"{BaseUrl}/find", filter);
        public virtual void Update(string id, TEntity entity) => GenericProxies.RestPostNonQuery($"{BaseUrl}/update", new { id = entity.Id, update = entity });
    }

}
