using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    internal abstract class BaseService<TEntity> : IService<TEntity> where TEntity : Entity, new ()
    { 
        protected IRepository<TEntity> Repository;
        protected IRepositoryBootstrapper RepositoryResolver;

        public BaseService(IRepositoryBootstrapper repositoryBootstrapper)
        {
            RepositoryResolver = repositoryBootstrapper;
            Repository = (IRepository<TEntity>)repositoryBootstrapper[typeof(TEntity)];//fix
        }

        public virtual List<TEntity> Find(SearchFilter<TEntity> filter) => filter.OptionList.Count() != 0 ? Repository.Find(filter) : new List<TEntity>();

        public virtual void Add(TEntity entity) => Repository.Add(entity);

        public virtual void Delete(string id) => Repository.Delete(id);

        public virtual void Update(string id, TEntity entity) => Repository.Update(id, entity);

    }
}

