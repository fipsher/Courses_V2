using Core.Entities;

namespace Data.Services
{
    internal class DisciplineService : BaseService<Discipline>, IDisciplineService
    {
        public DisciplineService(RepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
        }
    }

    internal interface IDisciplineService
    {
    }
}
