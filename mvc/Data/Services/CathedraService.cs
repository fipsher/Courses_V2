using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces;

namespace Data.Services
{
    class CathedraService : BaseService<Cathedra>, ICathedraService
    {
        public CathedraService(IRepositoryBootstrapper repositoryBootstrapper) : base(repositoryBootstrapper)
        {
        }
    }
}
