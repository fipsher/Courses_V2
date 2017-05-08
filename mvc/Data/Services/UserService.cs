using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using System;
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
        
        public bool TryAdd(User entity)
        {
            var loginExist = Find(new SearchFilter<User>
            {
                OptionList = new[] { new User { Login = entity.Login } }
            }).Any();
            if (!loginExist)
            {
                var group = _groupService.Find(SearchFilter<Group>.FilterById(entity.GroupId)).SingleOrDefault();
                entity.Course = group.Course;
                Add(entity);
            }
            return !loginExist;
        }
    }
}
