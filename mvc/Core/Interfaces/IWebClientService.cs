using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IWebClientService<TEntity, TId> where TEntity : IEntity<TId>
    {
        dynamic Get(object param);
        dynamic Post(object param);
        void Delete(object param);
    }
}
