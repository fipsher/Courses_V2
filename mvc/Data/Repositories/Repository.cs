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

        public void Add(TEntity entity) => throw new NotImplementedException();
        public void Delete(TId id) => throw new NotImplementedException();
        public TEntity Find(TId id) => throw new NotImplementedException();
        public void Update(TEntity entity) => throw new NotImplementedException();
    }
}
