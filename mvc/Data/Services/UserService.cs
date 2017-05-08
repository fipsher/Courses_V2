using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using System.Linq;

namespace Data.Services
{
    internal class UserService : BaseService<User>, IUserService
    {
        private readonly IRepository<Group> _groupService;

        public UserService(IRepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
            _groupService = (IRepository<Group>)repositoryStrategy[typeof(Group)];
        }

        public override void Add(User entity)
        {
            var group = _groupService.Find(SearchFilter<Group>.FilterById(entity.GroupId)).SingleOrDefault();

            entity.Course = group.Course;
        }
    }
}
