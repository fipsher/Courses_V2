using Core.Entities;
using Core.Helpers;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> : IRepository where TEntity : Entity, new ()
    {
        List<TEntity> Find(SearchFilter<TEntity> filter);

        void Add(TEntity entity);

        void Delete(string id);

        void Update(TEntity entity);
    }

    public interface IRepository{}
}
