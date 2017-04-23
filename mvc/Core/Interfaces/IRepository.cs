using Core.Helpers;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository { }

    public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        List<TEntity> Find(BaseSearchFilter<TEntity> filter);

        void Add(TEntity entity);

        void Delete(string id);

        void Update(TEntity entity);
    }
}
