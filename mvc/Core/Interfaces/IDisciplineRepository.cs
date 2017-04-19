using Core.Entities;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IDisciplineRepository : IRepository<Discipline, Guid>
    {
        IEnumerable<Discipline> Get(int take, int skip, string nameFilter);
    }
}