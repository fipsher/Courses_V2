using Core.Entities;
using Core.Interfaces;
using System;

namespace Data.Repositories
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        //public Repository(IWebClientService service)
        //{
        //    _service = service
        //}



        public virtual void Add(TEntity entity) => throw new NotImplementedException();
        public virtual void Delete(TId id) => throw new NotImplementedException();
        public virtual TEntity Find(TId id) => throw new NotImplementedException();
        public virtual void Update(TEntity entity) => throw new NotImplementedException();
    }
}
