using Core.Entities;
using Core.Interfaces.Services;

namespace Data.Services
{
    internal class UserService : BaseService<User>, IUserService
    {
        public UserService(RepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
        }
    }
}
