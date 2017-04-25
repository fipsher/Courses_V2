using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace Data.Services
{
    class StudentGroupService : BaseService<StudentGroup>, IStudentGroupService
    {
        public StudentGroupService(IRepositoryBootstrapper repositoryBootstrapper) : base(repositoryBootstrapper)
        {
        }
    }
}
