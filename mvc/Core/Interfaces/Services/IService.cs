using Core.Entities;
using Core.Helpers;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IService<TEntity> where TEntity : Entity, new ()
    {
        List<TEntity> Find(SearchFilter<TEntity> filter);

        void Add(TEntity entity);

        void Delete(string id);

        void Update(string id, TEntity entity);
    }
}
