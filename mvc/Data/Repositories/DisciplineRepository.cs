using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Data.Repositories
{
    class DisciplineRepository : Repository<Discipline, Guid>, IDisciplineRepository
    {
        public DisciplineRepository(string webApiUrl) : base(webApiUrl)
        {
        }

        public IEnumerable<Discipline> Get(int take, int skip, string nameFilter) => throw new NotImplementedException();
    }
}
