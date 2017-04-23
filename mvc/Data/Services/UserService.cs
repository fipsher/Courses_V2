using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace Data.Services
{
    internal class UserService : BaseService<User>, IUserService
    {
        public UserService(IRepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
        }
    }
}
