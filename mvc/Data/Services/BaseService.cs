using Core.Helpers;
using Core.Interfaces;
using System.Collections.Generic;

namespace Data.Services
{
    internal abstract class BaseService<TEntity> : IService<TEntity> where TEntity : class, IEntity
    { 
        protected IRepository<TEntity> Repository;
        protected IRepositoryBootstrapper RepositoryResolver;

        public BaseService(IRepositoryBootstrapper repositoryBootstrapper)
        {
            RepositoryResolver = repositoryBootstrapper;
            Repository = (IRepository<TEntity>)repositoryBootstrapper[typeof(TEntity)];//fix
        }

        public List<TEntity> Find(BaseSearchFilter<TEntity> filter) => Repository.Find(filter);

        public void Add(TEntity entity) => Repository.Add(entity);

        public void Delete(string id) => Repository.Delete(id);

        public void Update(TEntity entity) => Repository.Update(entity);

    }
}

