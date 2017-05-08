using Core.Entities;
using Core.Helpers;
using Core.Responces;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IGroupService : IService<Group>
    {
        IEnumerable<GroupResponce> FindGroupResponce(SearchFilter<Group> filter);
    }
}
