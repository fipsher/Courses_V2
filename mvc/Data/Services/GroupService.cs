﻿using Core.Entities;
using Core.Responces;
using Core.Interfaces;
using Core.Interfaces.Services;
using System.Collections.Generic;
using Core.Helpers;
using System.Linq;

namespace Data.Services
{
    class GroupService : BaseService<Group>, IGroupService
    {
        private readonly IRepository<Cathedra> _cathedraRepo;

        public GroupService(IRepositoryBootstrapper repositoryBootstrapper) : base(repositoryBootstrapper)
        {
            _cathedraRepo = (IRepository<Cathedra>)repositoryBootstrapper[typeof(Cathedra)];
        }

        public IEnumerable<GroupResponce> FindGroupResponce(SearchFilter<Group> filter)
        {
            IEnumerable<GroupResponce> result = new List<GroupResponce>();
            List<Cathedra> cathedras = new List<Cathedra>();
            var groups = Find(filter);

            var cathedraIds = groups.Select(g => g.CathedraId).Distinct().ToList();
            if (cathedraIds != null && cathedraIds.Any())
            {
                cathedras = _cathedraRepo.Find(SearchFilter<Cathedra>.FilterByIds(cathedraIds));
            }
            result = groups.Select(g => new GroupResponce(g)
            {
                Cathedra = cathedras.SingleOrDefault(c => c.Id == g.CathedraId)
            });
            return result;
        }
    }
}
