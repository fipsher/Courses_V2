using Core.Entities;
using Core.Helpers;
using Core.Responces;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IGroupService : IService<Group>
    {
        IEnumerable<GroupResponce> FindGroupResponce(SearchFilter<Group> filter);
        IEnumerable<GroupDisciplineModel> GetGroupDisciplines(string id);
        IEnumerable<Discipline> GetNotSubscribedDisciplines(string id);
        void AddDiscipline(string groupId, string disciplineId);
        void RemoveDisciplineFromGroup(string groupId, string disciplineId);
    }
}
