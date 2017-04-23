using Core.Entities;
using Core.Interfaces.Services;

namespace Data.Services
{
    internal class DisciplineService : BaseService<Discipline>, IDisciplineService
    {
        public DisciplineService(RepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
        }
    }
}
