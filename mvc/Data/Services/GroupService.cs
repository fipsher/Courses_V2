using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace Data.Services
{
    class GroupService : BaseService<Group>, IGroupService
    {
        public GroupService(IRepositoryBootstrapper repositoryBootstrapper) : base(repositoryBootstrapper)
        {
        }
    }
}
