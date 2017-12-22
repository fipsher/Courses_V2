using Core.Entities;
using Core.Helpers;
using Core.Responces;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IDisciplineService : IService<Discipline>
    {
        List<GroupDisciplineModel> FindDisciplineResponse(SearchFilter<Discipline> filter);

        List<User> GetDisciplineStudents(string disciplineId);

        bool TryRegisterStudent(string studentId, string disciplineId);

        bool UnregisterStudent(string studentId, string disciplineId);
    }
}
