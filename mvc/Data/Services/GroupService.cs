using Core.Entities;
using Core.Responces;
using Core.Interfaces;
using Core.Interfaces.Services;
using System.Collections.Generic;
using Core.Helpers;
using System.Linq;
using Core.Enums;

namespace Data.Services
{
    class GroupService : BaseService<Group>, IGroupService
    {
        private readonly IRepository<Cathedra> _cathedraRepo;
        private readonly IRepository<Discipline> _disciplineRepo;
        private readonly IRepository<User> _userRepo;

        public GroupService(IRepositoryBootstrapper repositoryBootstrapper) : base(repositoryBootstrapper)
        {
            _cathedraRepo = (IRepository<Cathedra>)repositoryBootstrapper[typeof(Cathedra)];
            _disciplineRepo = (IRepository<Discipline>)repositoryBootstrapper[typeof(Discipline)];
            _userRepo = (IRepository<User>)repositoryBootstrapper[typeof(User)];
        }

        public void AddDiscipline(string groupId, string disciplineId)
        {
            var group = Repository.Find(SearchFilter<Group>.FilterById(groupId)).SingleOrDefault();
            var discipline = _disciplineRepo.Find(SearchFilter<Discipline>.FilterById(disciplineId)).SingleOrDefault();

            if (group.DisciplineSubscriptions == null
                || group.DisciplineSubscriptions.All(el => el != disciplineId)
                && discipline.DisciplineType == Enums.DisciplineType.Special
                && (discipline.Semester == group.Course * 2
                    || discipline.Semester == group.Course * 2 + 1))
            {
                group.DisciplineSubscriptions = group.DisciplineSubscriptions ?? new List<string>();

                group.DisciplineSubscriptions.Add(disciplineId);
                Repository.Update(group.Id, group);
            }
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
        public IEnumerable<GroupDisciplineModel> GetGroupDisciplines(string id)
        {
            var group = Repository.Find(SearchFilter<Group>.FilterById(id)).SingleOrDefault();

            var disciplines =
                    group.DisciplineSubscriptions.Any()
                    ? _disciplineRepo.Find(SearchFilter<Discipline>.FilterByIds(group.DisciplineSubscriptions))
                    : new List<Discipline>();
            List<User> lecturers = new List<User>();
            List<Cathedra> cathedras = new List<Cathedra>();
            if (disciplines.Any())
            {
                var userIds = disciplines.Select(el => el.LecturerId);
                lecturers = _userRepo.Find(SearchFilter<User>.FilterByIds(userIds));

                var cathedraIds = disciplines.Select(el => el.ProviderCathedraId);
                cathedras = _cathedraRepo.Find(SearchFilter<Cathedra>.FilterByIds(cathedraIds));
            }
          

            return disciplines.Select(d => new GroupDisciplineModel
            {
                Id = d.Id,
                Name = d.Name,
                DisciplineType = d.DisciplineType,
                Semester = d.Semester,
                Description = d.Description,
                Lecturer = lecturers.SingleOrDefault(el => el.Id == d.LecturerId)?.UserName,
                ProviderCathedra = cathedras.SingleOrDefault(el => el.Id == d.ProviderCathedraId)?.Name
            });
        }

        public IEnumerable<Discipline> GetNotSubscribedDisciplines(string id, string name)
        {
            var group = Repository.Find(SearchFilter<Group>.FilterById(id)).SingleOrDefault();
            var assignedDisciplines =
                         group.DisciplineSubscriptions.Any()
                         ? _disciplineRepo.Find(SearchFilter<Discipline>.FilterByIds(group.DisciplineSubscriptions))
                         : new List<Discipline>();

            var filter = SearchFilter<Discipline>.Empty;
            filter.OptionList = FilterHelper.SpecialDisciplines(group.Course, name);
            var disciplines = _disciplineRepo
                .Find(filter)
                .Where(el => assignedDisciplines.All(ad => ad.Id != el.Id));

            return disciplines;
        }

        public void RemoveDisciplineFromGroup(string groupId, string disciplineId)
        {
            var group = Repository.Find(SearchFilter<Group>.FilterById(groupId)).SingleOrDefault();
            var discipline = _disciplineRepo.Find(SearchFilter<Discipline>.FilterById(disciplineId)).SingleOrDefault();

            group.DisciplineSubscriptions = group.DisciplineSubscriptions.Where(el => el != discipline.Id).ToList();
            Repository.Update(group.Id, group);
        }
    }
}
